using GameworldHandling;
using Singletons;
using UnityEngine;
using Utility;

namespace WavePlayer
{
    public class WaveHandler : MonoBehaviour
    {

        public static WaveHandler Instance;

        public float highestAcceptableAngle = 5;

        void Awake()
        {
            Instance = this;
        }

        public void TryToWave()
        {
            //bool successfullWave = false;
            byte succesfulWaves = 0;

            GameObject train = GetClosestTrain();

            System.Collections.Generic.List<GameObject> waveTargets = train.GetComponent<Train>().waveTargets;

            if(waveTargets.Count == 0)
            {
                WaveFailed();
                return;
            }

            foreach(GameObject go in waveTargets)
            {
                WaveTarget target = go.GetComponent<WaveTarget>();

                if(target.hasWaved)
                    continue;

                if(target.isLegalWave)
                {
                    succesfulWaves++;
                    target.hasWaved = true;
                }
            }

            if(succesfulWaves > 0)
            {
                WaveConnected(succesfulWaves);
            }
            else
            {
                WaveFailed();
            }
        }

        protected GameObject GetClosestTrain()
        {
            Queue<GameObject> trains = TrainHandler.Instance.trainQueue;

            //Check which train is the closest, that is the only one we have to check
            int closestTrainIndex = 0;
            float closestDistance = float.MaxValue;
            Vector3 playerPos = Player.Instance.transform.position;
            for (int i = 0; i < trains.Count; i++)
            {
                float distance = (trains.GetItem(i).transform.position - playerPos).magnitude;
                if (distance < closestDistance)
                {
                    closestTrainIndex = i;
                    closestDistance = distance;
                }
            }

            return trains.GetItem(closestTrainIndex);
        }

        public void WaveFailed()
        {
            StatKeeper.Instance.AddMiss();

            print("Wave missed");
        }

        protected void WaveConnected(byte successfulWaves)
        {
            StatKeeper.Instance.AddWaves(successfulWaves);

            print("Wave hit");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameworldHandling
{
    public class Train : MonoBehaviour
    {
        public List<GameObject> waveTargets = new List<GameObject>();

        public List<GameObject> windows = new List<GameObject>();

        public bool isVisible = false;

        void OnBecameVisible()
        {
            isVisible = true;
        }

        void OnBecameInvisible()
        {
            if(isVisible)
                TrainHandler.Instance.LoopTrain();
        }

        public void MoveTrainPiece(float xMove)
        {
            Vector3 position = transform.localPosition;
            position.x += xMove;

            transform.localPosition = position;
        }

        public void AddWavetarget(GameObject go)
        {
            waveTargets.Add(go);
        }

        public void ClearWavetargets()
        {
            foreach(GameObject go in waveTargets)
            {
                GameObject.Destroy(go);
            }

            waveTargets.Clear();
        }
    }
}
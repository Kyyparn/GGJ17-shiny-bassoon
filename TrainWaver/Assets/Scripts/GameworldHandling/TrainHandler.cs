using Singletons;
using UnityEngine;
using Utility;

namespace GameworldHandling
{
    public class TrainHandler : MonoBehaviour
    {
        protected const byte TrainBufferSize = 3;
        protected const byte maxWavetargetsPerGenBase = 4;

        public static TrainHandler Instance;

        private byte maxWavetargetsPerGen;

        public float trainSpeedBase = 3;
        public int speedIncreaseAtXSecond = 30;

        public Queue<GameObject> trainQueue = new Queue<GameObject>();

        public GameObject trainPrefab;
        public GameObject waveTargetPrefab;

        public bool isMoving = true;

        void Awake()
        {
            Instance = this;
        }

        // Use this for initialization
        void Start()
        {
            InitializeTrain();
            maxWavetargetsPerGen = maxWavetargetsPerGenBase;
        }

        // Update is called once per frame
        void Update()
        {
            if (isMoving)
            {
                float move = CalculateMove();

                for (int i = 0; i < trainQueue.Count; i++)
                {
                    trainQueue.GetItem(i).GetComponent<Train>().MoveTrainPiece(-move);
                }
            }
        }

        public void Reset()
        {
            isMoving = true;

            for (int i = 0; i < trainQueue.Count; i++)
            {
                trainQueue.GetItem(i).GetComponent<Train>().ClearWavetargets();
            }

            maxWavetargetsPerGen = maxWavetargetsPerGenBase;
        }

        void InitializeTrain()
        {
            float baseX = 0;

            for (int i = 0; i < TrainBufferSize; i++)
            {
                GameObject go = Instantiate(trainPrefab);
                Vector3 position = new Vector3(baseX + 10 * go.transform.lossyScale.x * i, 0);

                go.transform.parent = this.transform;
                go.transform.localPosition = position;

                trainQueue.Enqueue(go);
            }
        }

        public void LoopTrain()
        {
            GameObject go = trainQueue.Dequeue();

            //Clear obstacles
            go.GetComponent<Train>().ClearWavetargets();

            Vector3 newPosition = go.transform.localPosition;
            Vector3 lastPosition = trainQueue.GetLast().transform.localPosition;

            newPosition.x = lastPosition.x + 10 * go.transform.lossyScale.x;

            go.transform.localPosition = newPosition;

            CreateWavetargets(go);

            trainQueue.Enqueue(go);
        }

        void CreateWavetargets(GameObject train)
        {
            System.Collections.Generic.List<GameObject> windowsCopy = new System.Collections.Generic.List<GameObject>(train.GetComponent<Train>().windows);

            int numOfWavetargets = Random.Range(1, maxWavetargetsPerGen + 1);

            if (numOfWavetargets > windowsCopy.Count)
                numOfWavetargets = windowsCopy.Count;

            for (int i = 0; i < numOfWavetargets; i++)
            {
                //Get which window the wave target should be in.
                int windowIndex = Random.Range(0, windowsCopy.Count);

                GameObject window = windowsCopy[windowIndex];

                GameObject go = GameObject.Instantiate(waveTargetPrefab);

                Vector3 position = window.transform.localPosition;
                go.transform.parent = train.transform;
                go.transform.localPosition = position;

                train.GetComponent<Train>().AddWavetarget(go);

                windowsCopy.RemoveAt(windowIndex);
            }
        }

        float CalculateMove()
        {
            return (trainSpeedBase + ((int)(StatKeeper.Instance.TimeInSeconds / speedIncreaseAtXSecond))) * Time.deltaTime;
        }
    }
}

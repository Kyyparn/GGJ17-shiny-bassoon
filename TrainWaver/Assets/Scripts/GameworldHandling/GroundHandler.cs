using Singletons;
using UnityEngine;
using Utility;

namespace GameworldHandling
{
    public class GroundHandler : MonoBehaviour
    {
        protected const byte GroundBufferSize = 4;
        protected const int maxObstaclesPerGenBase = 2;

        private int maxObstaclesPerGen;

        public float groundSpeedBase = 3;
        public int speedIncreaseAtXSecond = 15;

        public static GroundHandler Instance;

        Queue<GameObject> groundQueue = new Queue<GameObject>();

        public GameObject[] lanes = new GameObject[3];

        public GameObject groundPrefab;
        public GameObject obstaclePrefab;

        public bool isMoving = true;

        void Awake()
        {
            Instance = this;
        }

        // Use this for initialization
        void Start()
        {
            InitializeGround();
            maxObstaclesPerGen = maxObstaclesPerGenBase;
        }

        void Update()
        {
            if (isMoving)
            {
                float move = CalculateMove();

                for (int i = 0; i < groundQueue.Count; i++)
                {
                    groundQueue.GetItem(i).GetComponent<Ground>().MoveGroundPiece(-move);
                }
            }
        }

        public void Reset()
        {
            isMoving = true;

            for(int i = 0; i < groundQueue.Count; i++)
            {
                groundQueue.GetItem(i).GetComponent<Ground>().ClearObstacles();
            }

            maxObstaclesPerGen = maxObstaclesPerGenBase;
        }

        void InitializeGround()
        {
            float baseX = -10;

            for(int i = 0; i < GroundBufferSize; i++)
            {
                Vector3 position = new Vector3(baseX + 10*i, 0);
                GameObject go = Instantiate(groundPrefab, position, Quaternion.identity, this.transform);
                go.transform.localPosition = position;
                groundQueue.Enqueue(go);
            }
        }

        public void LoopGround()
        {
            GameObject go = groundQueue.Dequeue();

            //Clear obstacles
            go.GetComponent<Ground>().ClearObstacles();

            Vector3 newPosition = go.transform.localPosition;
            Vector3 lastPosition = groundQueue.GetLast().transform.localPosition;

            newPosition.x = lastPosition.x + 10;

            go.transform.localPosition = newPosition;

            CreateObstacles(go);

            groundQueue.Enqueue(go);
        }

        void CreateObstacles(GameObject ground)
        {
            int numOfObstacles = Random.Range(1, maxObstaclesPerGen + 1);

            for (int i = 0; i < numOfObstacles; i++)
            {
                GameObject go = GameObject.Instantiate(obstaclePrefab);

                //Get which lane
                int lane = Random.Range(0, 3);

                //Randomize x position
                float xPos = ground.transform.position.x + Random.Range(-4, 5);
                float zPos = lanes[lane].transform.position.z - 0.2f;
                float yPos = ground.transform.position.y + 0.80f;

                Vector3 position = new Vector3(xPos, yPos, zPos);
                go.transform.parent = ground.transform;
                go.transform.position = position;

                ground.GetComponent<Ground>().AddObstacle(go);
            }
        }

        float CalculateMove()
        {
            return (groundSpeedBase + ((int)(StatKeeper.Instance.TimeInSeconds / speedIncreaseAtXSecond))) * Time.deltaTime;
        }

    }
}

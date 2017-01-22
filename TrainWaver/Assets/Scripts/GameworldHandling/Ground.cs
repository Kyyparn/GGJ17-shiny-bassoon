using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameworldHandling
{
    public class Ground : MonoBehaviour
    {
        private List<GameObject> obstacles = new List<GameObject>();

        bool wasVisible = false;

        void OnBecameVisible()
        {
            wasVisible = true;
        }

        void OnBecameInvisible()
        {
            if(wasVisible)
                GroundHandler.Instance.LoopGround();
        }

        public void MoveGroundPiece(float xMove)
        {
            Vector3 position = transform.localPosition;
            position.x += xMove;

            transform.localPosition = position;
        }

        public void AddObstacle(GameObject go)
        {
            obstacles.Add(go);
        }

        public void ClearObstacles()
        {
            foreach(GameObject go in obstacles)
            {
                GameObject.Destroy(go);
            }

            obstacles.Clear();
        }
    }
}
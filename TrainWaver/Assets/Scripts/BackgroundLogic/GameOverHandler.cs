using GameworldHandling;
using Singletons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WavePlayer;

namespace BackgroundLogic
{
    public class GameOverHandler : MonoBehaviour
    {
        public static GameOverHandler Instance;

        void Awake()
        {
            Instance = this;
        }

        public void GameOver()
        {
            print("Game over");
            print("SURVIVED: " + StatKeeper.Instance.TimeInSeconds + " SECONDS");

            TrainHandler.Instance.isMoving = false;
            GroundHandler.Instance.isMoving = false;

            Player.Instance.FallAnimator.PlayAnimation();
        }
    }
}

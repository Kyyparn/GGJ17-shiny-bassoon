using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WavePlayer
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        public PlayerMovement Movement;
        public FallAnimator FallAnimator;
        public AnimationLooper AnimationLooper;

        void Awake()
        {
            Instance = this;
        }

        public void Reset()
        {
            AnimationLooper.enabled = true;
            Movement.Reset();
        }
    }
}

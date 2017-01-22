using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WavePlayer
{
    public class AnimationLooper : MonoBehaviour
    {
        public enum AnimationStates { Run, Wave };

        public Texture[] animationFramesRunning;
        public Texture[] animationFramesWaving;

        public AnimationStates currentAnimation;
        private Texture[] currentAnimationFrames;

        public int frameRate;
        byte currentIndex = 0;

        float timeBetweenFrames;
        DateTime lastFrameTime;

        public bool GoBackToRun;

        void Start()
        {
            if (currentAnimation == AnimationStates.Run)
                currentAnimationFrames = animationFramesRunning;
            else
                currentAnimationFrames = animationFramesWaving;

            timeBetweenFrames = 1.0f / frameRate;
            ShowNextFrame();
            lastFrameTime = DateTime.UtcNow;
        }

        void Update()
        {
            if ((DateTime.UtcNow - lastFrameTime).TotalSeconds > timeBetweenFrames)
            {
                ShowNextFrame();
                lastFrameTime = DateTime.UtcNow;
            }
        }

        void ShowNextFrame()
        {
            this.gameObject.GetComponent<MeshRenderer>().material.mainTexture = currentAnimationFrames[currentIndex];

            currentIndex++;

            if (currentIndex >= currentAnimationFrames.Length)
            {
                if (currentAnimation == AnimationStates.Wave)
                {
                    if (GoBackToRun)
                    {
                        SwitchAnimation(AnimationStates.Run);
                        return;
                    }
                }

                currentIndex = 0;
            }
        }

        public void SwitchAnimation(AnimationStates state)
        {
            currentAnimation = state;

            if (state == AnimationStates.Run)
                currentAnimationFrames = animationFramesRunning;
            else if(state == AnimationStates.Wave)
                currentAnimationFrames = animationFramesWaving;

            currentIndex = 0;
        }
    }
}

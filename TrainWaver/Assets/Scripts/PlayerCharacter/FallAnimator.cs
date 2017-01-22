using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WavePlayer
{
    public class FallAnimator : MonoBehaviour 
    {
        public Texture[] animationFrames;

        public int frameRate;
        byte currentIndex = 0;

        float timeBetweenFrames;
        DateTime lastFrameTime;

        private bool playAnimaton = false;
        public bool loop = false;

        void Start()
        {
            timeBetweenFrames = 1.0f / frameRate;
            lastFrameTime = DateTime.UtcNow;
        }

        void Update()
        {
            if (playAnimaton)
            {
                if ((DateTime.UtcNow - lastFrameTime).TotalSeconds > timeBetweenFrames)
                {
                    ShowNextFrame();
                    lastFrameTime = DateTime.UtcNow;
                }
            }
        }

        void ShowNextFrame()
        {
            this.gameObject.GetComponent<MeshRenderer>().material.mainTexture = animationFrames[currentIndex];

            currentIndex++;

            if (currentIndex >= animationFrames.Length)
            {
                if (loop)
                    currentIndex = 0;
                else
                    playAnimaton = false;
            }
        }

        public void PlayAnimation()
        {
            AnimationLooper looper = this.gameObject.GetComponent<AnimationLooper>();

            if (looper != null)
                looper.enabled = false;

            currentIndex = 0;
            playAnimaton = true;
            ShowNextFrame();
            lastFrameTime = DateTime.UtcNow;

            FallAudioHandler.Instance.PlayRandomSound();
        }
    }
}

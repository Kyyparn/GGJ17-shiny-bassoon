using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameworldHandling;

namespace WavePlayer
{
    public class PlayerMovement : MonoBehaviour
    {


        //Inner lane = 0;
        //Mid lane = 1;
        //Outer lane = 2;
        public byte _currentLane = 1;

        void Start()
        {
            CurrentLane = _currentLane;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                DecreaseLane();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                IncreaseLane();   
            }
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                this.gameObject.GetComponent<HelloAudioHandler>().PlayRandomSound();
                this.gameObject.GetComponent<AnimationLooper>().SwitchAnimation(AnimationLooper.AnimationStates.Wave);
                WaveHandler.Instance.TryToWave();
            }
        }

        void IncreaseLane()
        {
            if (CurrentLane < 2)
            {
                CurrentLane++;
            }
        }

        void DecreaseLane()
        {
            if (CurrentLane > 0)
            {
                CurrentLane--;
            }
        }

        public byte CurrentLane
        {
            get
            {
                return _currentLane;
            }
            set
            {
                _currentLane = value;
                MovePlayerToCurrentLane();
            }
        }

        void MovePlayerToCurrentLane()
        {
            Vector3 newPosition = transform.position;
            newPosition.z = GroundHandler.Instance.lanes[CurrentLane].transform.position.z;
            transform.localPosition = newPosition;
        }

        public void Reset()
        {
            CurrentLane = 1;
        }
    }
}

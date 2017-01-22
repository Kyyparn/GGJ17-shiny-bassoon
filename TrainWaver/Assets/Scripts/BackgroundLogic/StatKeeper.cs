using BackgroundLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singletons
{
    public class StatKeeper : MonoBehaviour
    {
        public static StatKeeper Instance;

        public int numOfLives = 5;
        public float MetersPerSecond;

        private uint _NumOfWaves;
        private uint _Distance;
        private DateTime StartTime;
        private uint _Score;
        private uint _Misses;
        private uint _TotalMisses;

        void Awake()
        {
            Instance = this;
            GameStart(); //TODO: Remove this, maybe
        }

        public void GameStart()
        {
            ResetValues();
            UILife.Instance.ShowLife((byte)(5 - Misses));
            StartTime = DateTime.UtcNow; 
        }

        void ResetValues()
        {
            NumOfWaves = 0;
            Score = 0;
            Misses = 0;
            TotalMisses = 0;
        }

        public void AddMiss()
        {
            Misses++;
            TotalMisses++;

            UILife.Instance.ShowLife((byte)(5 - Misses));

            if(Misses >= numOfLives)
            {
                GameOverHandler.Instance.GameOver();
            }
        }

        public void RemoveMiss()
        {
            Misses--;
        }

        public void AddWave()
        {
            NumOfWaves++;
        }

        public void AddWaves(byte numOfWaves)
        {
            NumOfWaves += numOfWaves;
        }

        #region Properties
        public uint NumOfWaves
        {
            get
            {
                return _NumOfWaves;
            }
            set
            {
                _NumOfWaves = value;
            }
        }

        public double Distance
        {
            get
            {
                return TimeInSeconds * MetersPerSecond;
            }
        }

        public double TimeInSeconds
        {
            get
            {
                return (DateTime.UtcNow - StartTime).TotalSeconds;
            }
        }

        public uint Score
        {
            get
            {
                return _Score; 
            }
            set
            {
                _Score = value;
            }
        }

        public uint Misses
        {
            get
            {
                return _Misses;
            }
            protected set
            {
                _Misses = value;
            }
        }

        public uint TotalMisses
        {
            get
            {
                return _TotalMisses;
            }
            protected set
            {
                _TotalMisses = value;
            }
        }
        #endregion
    }
}
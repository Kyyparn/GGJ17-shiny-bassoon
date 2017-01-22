using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WavePlayer;

public class WaveTarget : MonoBehaviour {

    public float triggerSize = 2.5f;

    bool _hasWaved = false;
    public bool isLegalWave = false;

    private Player player;

    void Start()
    {
        player = Player.Instance;
    }


    void FixedUpdate()
    {
        float playerX = player.transform.position.x;
        float targetX = this.transform.position.x;

        float distance = Mathf.Abs(playerX - targetX);

        if(distance < triggerSize)
        {
            isLegalWave = true;
        }
        else
        {
            if(isLegalWave) //If it was legal last update
            {
                SetWaveLegalFalse();
            }
        }
    }

    public void SetWaveLegalFalse()
    {
        if(!hasWaved)
        {
            WaveHandler.Instance.WaveFailed();
        }
        isLegalWave = false;
    }

    public bool hasWaved
    {
        get
        {
            return _hasWaved;
        }
        set
        {
            _hasWaved = value;

            if (value)
                this.GetComponent<AnimationLooper>().SwitchAnimation(AnimationLooper.AnimationStates.Wave);
        }
    }
}

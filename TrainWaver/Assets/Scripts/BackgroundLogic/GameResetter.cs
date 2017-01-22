using GameworldHandling;
using Singletons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WavePlayer;

public class GameResetter : MonoBehaviour {

    public static GameResetter Instance;

    void Awake()
    {
        Instance = this;
    }

	void Update () {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
	}

    public void GameOver()
    {

    }

    public void ResetGame()
    {
        print("RESET");
        Player.Instance.Reset();

        GroundHandler.Instance.Reset();
        TrainHandler.Instance.Reset();

        StatKeeper.Instance.GameStart();
    }
}

using BackgroundLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelpScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameOverHandler.Instance.GameOver();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
        {
            this.gameObject.SetActive(false);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILife : MonoBehaviour {

    public static UILife Instance;

    public Image[] images;

    void Awake()
    {
        Instance = this;
    }

    public void ShowLife(byte life)
    {
        foreach(Image img in images)
        {
            img.gameObject.SetActive(false);
        }

        for(int i = 0; i < life; i++)
        {
            images[i].gameObject.SetActive(true);
        }
    }
}

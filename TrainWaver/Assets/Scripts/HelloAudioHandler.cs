using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloAudioHandler : MonoBehaviour {

    public static HelloAudioHandler Instance;

    public AudioClip[] helloClips;

    void Awake()
    {
        Instance = this;
    }

    public void PlayRandomSound()
    {
        int rand = Random.Range(0, helloClips.Length);

        AudioClip clip = helloClips[rand];

        Singletons.AudioPlayer.Instance.PlayAudio(clip);
    }
}

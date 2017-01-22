using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAudioHandler : MonoBehaviour {

    public static FallAudioHandler Instance;

    public AudioClip[] fallClips;

    void Awake()
    {
        Instance = this;
    }

    public void PlayRandomSound()
    {
        int rand = Random.Range(0, fallClips.Length);

        AudioClip clip = fallClips[rand];

        Singletons.AudioPlayer.Instance.ForcePlayAudio(clip);
    }
}

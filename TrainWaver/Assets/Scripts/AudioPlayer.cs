using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singletons
{
    public class AudioPlayer : MonoBehaviour
    {
        public static AudioPlayer Instance;

        void Awake()
        {
            Instance = this;
        }

        public void PlayAudio(AudioClip clip)
        {
            AudioSource source = this.GetComponent<AudioSource>();
            
                if(!source.isPlaying)
                {
                    source.clip = clip;
                    this.GetComponent<AudioSource>().Play();
                }
        }

        public void ForcePlayAudio(AudioClip clip)
        {
            AudioSource source = this.GetComponent<AudioSource>();

            source.clip = clip;
            this.GetComponent<AudioSource>().Play();
        }
    }
}

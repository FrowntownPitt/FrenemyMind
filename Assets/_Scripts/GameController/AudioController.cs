using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GC
{
    public class AudioController : MonoBehaviour
    {

        public enum Sources
        {
            Background,
            PlayerShooter,
            Explosion,
            Movement,
            Misfire,
            EnemyShooter
        };

        [NamedArrayAttribute(new string[] { "Background Music"
            , "Player Shooter", "Explosion", "Player Engines"
            , "Misfire", "Enemy Shooter" })]
        //[NamedArray(Enum.GetNames(typeof(Sources))]
        public AudioSource[] audioSources;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayAudio(Sources source)
        {
            audioSources[(int)source].Play();
        }

        public void StopAudio(Sources source)
        {
            audioSources[(int)source].Stop();
        }

        public void ToggleAudioContinuous(Sources source, bool toggle)
        {
            audioSources[(int)source].loop = toggle;
        }

        public void SetAudioVolume(Sources source, float level)
        {
            audioSources[(int)source].volume = level;
        }
    }
    
}
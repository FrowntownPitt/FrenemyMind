using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GC
{
    // Container class for all audio sources
    public class AudioController : MonoBehaviour
    {
        // All possible audio sources (mapped to the array of sources)
        public enum Sources
        {
            Background,
            PlayerShooter,
            Explosion,
            Movement,
            Misfire,
            EnemyShooter
        };

        // Create an array whose indices are labeled in the editor
        [NamedArrayAttribute(new string[] { "Background Music"
            , "Player Shooter", "Explosion", "Player Engines"
            , "Misfire", "Enemy Shooter" })]
        //[NamedArray(Enum.GetNames(typeof(Sources))]
        public AudioSource[] audioSources;
        

        // Play the given audio source's sound
        public void PlayAudio(Sources source)
        {
            audioSources[(int)source].Play();
        }

        // Stop the given audio source's sound
        public void StopAudio(Sources source)
        {
            audioSources[(int)source].Stop();
        }

        // Toggle the source's loop flag
        public void ToggleAudioContinuous(Sources source, bool toggle)
        {
            audioSources[(int)source].loop = toggle;
        }

        // Change a source's volume
        public void SetAudioVolume(Sources source, float level)
        {
            audioSources[(int)source].volume = level;
        }
    }
    
}
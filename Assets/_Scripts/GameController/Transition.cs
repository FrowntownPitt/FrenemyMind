using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC
{
    // Transition scene manager
    public class Transition : MonoBehaviour
    {
        public float transitionTime;
        
        void Start()
        {
            StartCoroutine(StartTransition());
        }
        
        // Transition to Level 2 after the set transition period
        IEnumerator StartTransition()
        {
            float startTime = Time.time;
            yield return new WaitForSeconds(transitionTime);
            // Wait the transition period
            while(Time.time - startTime <= transitionTime)
            {
                yield return new WaitForEndOfFrame();
            }

            // Use the sceneLoader's scene swap to load Level 2
            FindObjectOfType<SceneLoader>().SwapScenes("Transition","Level2");
        }
    }
}
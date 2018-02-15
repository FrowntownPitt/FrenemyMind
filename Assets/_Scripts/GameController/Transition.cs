using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC
{
    public class Transition : MonoBehaviour
    {
        public float transitionTime;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(StartTransition());
        }
        
        IEnumerator StartTransition()
        {
            float startTime = Time.time;
            yield return new WaitForSeconds(transitionTime);
            while(Time.time - startTime <= transitionTime)
            {
                yield return new WaitForEndOfFrame();
            }

            FindObjectOfType<SceneLoader>().SwapScenes("Transition","Level2");
        }
    }
}
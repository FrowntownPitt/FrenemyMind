using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GC {
    public class SceneLoader : MonoBehaviour {

        public List<GameObject> keepObjects;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void LoadScene(string name)
        {
            GameObject[] objects = FindObjectsOfType<GameObject>() as GameObject[];

            for(int i=0; i<objects.Length; i++)
            {
                if (keepObjects.Contains(objects[i]))
                {

                }
                else
                {
                    Destroy(objects[i]);
                }
            }

            
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }

        public void UnloadScene(string name)
        {
            UnloadScene(name);
        }
    }
}
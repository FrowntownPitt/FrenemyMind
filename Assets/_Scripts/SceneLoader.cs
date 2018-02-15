using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GC {
    public class SceneLoader : MonoBehaviour {

        public List<GameObject> keepObjects;

        public string activeScene;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void DisableSceneElements()
        {
            //GameObject[] objects = FindObjectsOfType<GameObject>() as GameObject[];
            Scene s = SceneManager.GetSceneByName("Main");
            GameObject[] objects = s.GetRootGameObjects();

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

        }

        public void LoadScene(string name)
        {
            activeScene = name;
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }

        public void UnloadScene(string name)
        {
            SceneManager.UnloadSceneAsync(name);
        }

        public void SwapScenes(string from, string to)
        {
            UnloadScene(from);
            LoadScene(to);
        }
    }
}
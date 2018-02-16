using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GC
{
    // Persistent Scene Manager
    public class SceneLoader : MonoBehaviour {

        // Objects to keep on DisableSceneElements()
        public List<GameObject> keepObjects;

        // Save the current loaded Additive scene
        public string activeScene;

        // Called to disable the Main scene elements that should not persist through the game
        public void DisableSceneElements()
        {
            //GameObject[] objects = FindObjectsOfType<GameObject>() as GameObject[];
            // Get all objects in the scene
            Scene s = SceneManager.GetSceneByName("Main");
            GameObject[] objects = s.GetRootGameObjects();

            // Iterate over all objects and deactivate elements for only the main scene
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

        // Load a scene additively by name
        public void LoadScene(string name)
        {
            activeScene = name;
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }

        // Unload a scene by name
        public void UnloadScene(string name)
        {
            SceneManager.UnloadSceneAsync(name);
        }

        // Swap an additive scene for another additive scene
        public void SwapScenes(string from, string to)
        {
            UnloadScene(from);
            LoadScene(to);
        }
    }
}
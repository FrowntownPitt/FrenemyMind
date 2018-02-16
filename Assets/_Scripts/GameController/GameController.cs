using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GC
{
    // Persistent GameController
    public class GameController : MonoBehaviour
    {
        public bool won = false;

        // Public facing scene change handler
        public void ChangeScene()
        {

            LevelController level = FindObjectOfType<LevelController>();

            switch (level.gameState)
            {
                case LevelController.GameState.won:
                    {
                        // Go from Main->Level1->Transition->Level2->End->Main
                        won = true;
                        if (FindObjectOfType<SceneLoader>().activeScene.Equals("Level1"))
                        {
                            Debug.Log("In Level 1");
                            FindObjectOfType<SceneLoader>().SwapScenes("Level1", "Transition");
                        }
                        else if(FindObjectOfType<SceneLoader>().activeScene.Equals("Level2"))
                        {
                            Debug.Log("In Level 2");
                            FindObjectOfType<SceneLoader>().SwapScenes("Level2", "End");
                        }
                        break;
                    }
                case LevelController.GameState.lost:
                    {
                        // If they lose (regardless of the scene) go to the end
                        won = false;
                        Debug.Log("Lost");
                        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
                        sceneLoader.SwapScenes(sceneLoader.activeScene, "End");
                        break;
                    }
                case LevelController.GameState.died:
                    {
                        won = false;
                        Debug.Log("Died");
                        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
                        Debug.Log(sceneLoader.activeScene);
                        sceneLoader.SwapScenes(sceneLoader.activeScene, "End");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
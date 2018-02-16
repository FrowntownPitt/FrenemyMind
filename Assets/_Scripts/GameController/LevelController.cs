using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC
{
    // Game Controller for the current level
    public class LevelController : MonoBehaviour
    {
        // Allow the player to still continue even if a few enemies arent destroyed
        public int lossThreshold;

        public bool gameOver;

        // There are certain states used to determine scene actions
        public enum GameState
        {
            playing,
            died,
            won,
            lost
        };

        // We start the game as playing
        public GameState gameState = GameState.playing;

        // Track the number of escaped enemies
        public int EscapedEnemies = 0;

        // Public EndLevel() is called to end the current level.
        public void EndLevel()
        {
            Debug.Log("GameState: " + gameState);
            // If player died then run the internal EndLevel() and signal it is game over
            if (gameState == GameState.died)
            {
                StartCoroutine(_EndLevel());
                gameOver = true;
            }
            // If all of the enemies have been spawned then wait for them to leave/die
            else
            {
                // Only call WaitForShips if the player hasn't died yet
                if(!gameOver)
                    StartCoroutine(WaitForShips());
            }

            //Debug.Log("GC: Ending level");
        }

        // Internal EndLevel()
        private IEnumerator _EndLevel()
        {
            Debug.Log("End of game");
            switch (gameState)
            {
                // All ending cases.
                case GameState.playing:
                    {
                        if (EscapedEnemies > lossThreshold)
                        {
                            Debug.Log("Lost! Escaped enemies: " + EscapedEnemies);
                            gameState = GameState.lost;
                        }
                        else if (EscapedEnemies > 0)
                        {
                            Debug.Log("Tis but a scratch! Escaped enemies: " + EscapedEnemies);
                            gameState = GameState.won;
                        }
                        else
                        {
                            Debug.Log("None escaped. Win!");
                            gameState = GameState.won;
                        }
                        FindObjectOfType<GameController>().ChangeScene();
                        break;
                    }
                case GameState.died:
                    {
                        gameOver = true;
                        Debug.Log("Escaped enemies: " + (EscapedEnemies +
                            GetComponent<EnemySpawner>().GetActiveEnemies() +
                            GetComponent<EnemySpawner>().GetRemainingEnemies()));
                        Debug.Log("Died");

                        // If too many enemies escaped then they lose.
                        if((EscapedEnemies +
                            GetComponent<EnemySpawner>().GetActiveEnemies() +
                            GetComponent<EnemySpawner>().GetRemainingEnemies()) > lossThreshold){
                            gameState = GameState.lost;
                        }
                        else
                        {
                            // If they died but saved the Earth they still win
                            if (FindObjectOfType<SceneLoader>().activeScene.Equals("Level2"))
                                gameState = GameState.won;
                            else
                                gameState = GameState.lost;
                        }

                        FindObjectOfType<GameController>().ChangeScene();

                        break;
                    }
                case GameState.won:
                    {

                        FindObjectOfType<GameController>().ChangeScene();
                        Debug.Log("All enemy ships cleared!");
                        break;
                    }
                default:
                    {
                        gameOver = true;
                        break;
                    }
            }

            yield return null;
        }

        public void Die()
        {
            //Debug.Log("Died");
            gameState = GameState.died;
            //EndLevel();
        }

        public void EnemyEscaped()
        {
            EscapedEnemies++;
        }

        // Wait to end the scene until the ships are all destroyed or escape
        IEnumerator WaitForShips()
        {
            EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
            int enemyships = spawner.GetActiveEnemies();

            while (enemyships > 0)//enemyships.Length > 0)
            {
                //Debug.Log("Enemies: " + enemyships.Length + enemyships[0].name);
                enemyships = spawner.GetActiveEnemies();

                yield return new WaitForSeconds(0.5f);
            }

            Debug.Log("GC: Ended level");

            if(gameState != GameState.died && !gameOver)
                StartCoroutine(_EndLevel());

            gameOver = true;

            yield return null;
        }
    }
}
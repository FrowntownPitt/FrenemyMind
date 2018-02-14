using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC
{
    public class GameController : MonoBehaviour
    {
        public int lossThreshold;

        public bool gameOver;
        public enum GameState
        {
            playing,
            died,
            won,
            lost
        };

        public GameState gameState = GameState.playing;

        public int EscapedEnemies = 0;

        public void EndLevel()
        {
            Debug.Log("GameState: " + gameState);
            //StartCoroutine(_EndLevel());
            //StartCoroutine(WaitForShips());
            if (gameState == GameState.died)
            {
                if(!gameOver)
                    StartCoroutine(_EndLevel());
                gameOver = true;
            }
            else
            {
                if(!gameOver)
                    StartCoroutine(WaitForShips());
            }

            //Debug.Log("GC: Ending level");
        }

        private IEnumerator _EndLevel()
        {
            Debug.Log("End of game");
            switch (gameState)
            {
                case GameState.playing:
                    {
                        if (EscapedEnemies > lossThreshold)
                        {
                            Debug.Log("Lost! Escaped enemies: " + EscapedEnemies);
                            gameState = GameState.won;
                        }
                        else if (EscapedEnemies > 0)
                        {
                            Debug.Log("Tis but a scratch! Escaped enemies: " + EscapedEnemies);
                            gameState = GameState.lost;
                        }
                        else
                        {
                            Debug.Log("None escaped. Win!");
                            gameState = GameState.won;
                        }
                        break;
                    }
                case GameState.died:
                    {
                        gameOver = true;
                        Debug.Log("Escaped enemies: " + (EscapedEnemies +
                            GetComponent<EnemySpawner>().GetActiveEnemies() +
                            GetComponent<EnemySpawner>().GetRemainingEnemies()));
                        Debug.Log("Died");
                        break;
                    }
                case GameState.won:
                    {
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
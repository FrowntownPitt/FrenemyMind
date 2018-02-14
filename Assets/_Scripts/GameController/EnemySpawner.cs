using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC
{
    public class EnemySpawner : MonoBehaviour
    {

        public GameObject enemyPrefab;

        public GameObject SpawnLocation;

        public GameController gameController;

        public float previousSpawnTime;

        public float spawnrate = 2f;
        public float randomSpawnRange = 1.9f;

        private float nextRandomTime = 0f;

        public int spawnAmount = 40;
        private int spawnCounter = 0;

        [SerializeField]
        protected int activeEnemies = 0;

        public bool isSpawning = true;

        // Use this for initialization
        void Start()
        {
            previousSpawnTime = Time.time;
            nextRandomTime = 0f;

            spawnCounter = 0;
        }

        public int GetRemainingEnemies()
        {
            return spawnAmount - spawnCounter;
        }

        public void Died()
        {
            activeEnemies--;
        }

        public int GetActiveEnemies()
        {
            return activeEnemies;
        }

        // Update is called once per frame
        void Update()
        {
            if (spawnCounter < spawnAmount && Time.time - previousSpawnTime > nextRandomTime)
            {
                spawnCounter++;
                activeEnemies++;
                //Debug.Log("Spawning");
                previousSpawnTime = Time.time;
                nextRandomTime = Random.Range(spawnrate - randomSpawnRange, spawnrate + randomSpawnRange);
                //Transform t = ChooseSpawnTransform();
                GameObject enemy = Instantiate(enemyPrefab);//, t.position, t.rotation);
                ChooseSpawnTransform(enemy.transform);
            }
            if (spawnCounter >= spawnAmount && isSpawning)
            {
                gameController.EndLevel();
                //Debug.Log("Ending level");
                isSpawning = false;
            }
        }

        void ChooseSpawnTransform(Transform t)
        {
            if (SpawnLocation != null)
            {
                float x = SpawnLocation.transform.position.x;
                float upperBound = SpawnLocation.transform.position.y + SpawnLocation.transform.localScale.y / 2;
                float lowerBound = SpawnLocation.transform.position.y - SpawnLocation.transform.localScale.y / 2;

                t.position = new Vector3(x, Random.Range(lowerBound, upperBound));
            }
            //return transform;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC
{
    // Enemy random spawner
    public class EnemySpawner : MonoBehaviour
    {

        public GameObject enemyPrefab;

        // Area which the enemies can spawn from random location within
        public GameObject SpawnLocation;

        public LevelController LevelController;

        public float previousSpawnTime;

        // Spawn with some randomness
        public float spawnrate = 2f;
        public float randomSpawnRange = 1.9f;

        private float nextRandomTime = 0f;

        // How many to spawn, and track how many have spawned
        public int spawnAmount = 40;
        private int spawnCounter = 0;

        [SerializeField]
        // Track how many enemies are currently on screen
        protected int activeEnemies = 0;

        public bool isSpawning = true;

        // Use this for initialization
        void Start()
        {
            previousSpawnTime = Time.time;
            nextRandomTime = 0f;

            spawnCounter = 0;
            LevelController = FindObjectOfType<LevelController>();
        }

        public int GetRemainingEnemies()
        {
            return spawnAmount - spawnCounter;
        }

        // When an enemy dies, it must call this to decrement the Active counter
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
                // Pick a new random time
                previousSpawnTime = Time.time;
                nextRandomTime = Random.Range(spawnrate - randomSpawnRange, spawnrate + randomSpawnRange);
                //Transform t = ChooseSpawnTransform();
                GameObject enemy = Instantiate(enemyPrefab, GameObject.Find("InstantiateContainer").transform);//, t.position, t.rotation);
                // Choose a location to spawn
                ChooseSpawnTransform(enemy.transform);
            }
            if (spawnCounter >= spawnAmount && isSpawning)
            {
                // Once all have been spawned, end the current level
                LevelController.EndLevel();
                //Debug.Log("Ending level");
                isSpawning = false;
            }
        }

        void ChooseSpawnTransform(Transform t)
        {
            if (SpawnLocation != null)
            {
                // Create a range of locations
                float x = SpawnLocation.transform.position.x;
                float upperBound = SpawnLocation.transform.position.y + SpawnLocation.transform.localScale.y / 2;
                float lowerBound = SpawnLocation.transform.position.y - SpawnLocation.transform.localScale.y / 2;

                // Create a random location based on the bounds
                t.position = new Vector3(x, Random.Range(lowerBound, upperBound));
            }
            //return transform;
        }
    }
}
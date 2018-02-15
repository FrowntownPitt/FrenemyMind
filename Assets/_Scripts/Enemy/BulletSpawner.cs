using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class BulletSpawner : MonoBehaviour
    {
        GC.AudioController audioController;

        //public Vector2 bulletSpeed;

        public GameObject bulletPrefab;

        public GameObject SpawnLocation;

        public float previousSpawnTime;

        public float spawnrate = 2f;
        public float randomSpawnRange = 1.9f;

        private float nextRandomTime = 0f;
        

        // Use this for initialization
        void Start()
        {
            audioController = FindObjectOfType<GC.AudioController>();
            previousSpawnTime = Time.time;
            nextRandomTime = 0f;
        }
        
        // Update is called once per frame
        void Update()
        {
            if (Time.time - previousSpawnTime > nextRandomTime)
            {
                //Debug.Log("Spawning");
                previousSpawnTime = Time.time;
                nextRandomTime = Random.Range(spawnrate - randomSpawnRange, spawnrate + randomSpawnRange);
                //Transform t = ChooseSpawnTransform();
                GameObject bullet = Instantiate(bulletPrefab, GameObject.Find("InstantiateContainer").transform);//, t.position, t.rotation);
                ChooseSpawnTransform(bullet.transform);

                Vector2 shipVelocity = GetComponent<Rigidbody2D>().velocity;

                bullet.GetComponent<Rigidbody2D>().velocity = shipVelocity;
                //bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed);

                audioController.PlayAudio(GC.AudioController.Sources.EnemyShooter);
            }
        }

        void ChooseSpawnTransform(Transform t)
        {
            if (SpawnLocation != null)
            {
                float x = SpawnLocation.transform.position.x;
                float y = SpawnLocation.transform.position.y;
                t.position = new Vector3(x,y);
            }
            //return transform;
        }
    }
}
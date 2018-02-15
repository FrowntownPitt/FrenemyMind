using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Shoot : MonoBehaviour
    {
        public BulletSpawner bulletSpawner;

        public GC.LevelController LevelController;

        // Use this for initialization
        void Start()
        {
            LevelController = FindObjectOfType<GC.LevelController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (LevelController.gameState == GC.LevelController.GameState.playing)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    bulletSpawner.TryFire();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    // The Player's shoot functionality
    public class Shoot : MonoBehaviour
    {
        public BulletSpawner bulletSpawner;
        
        public GC.LevelController LevelController;

        // Use this for initialization
        void Start()
        {
            // Cache the scene's LevelController
            LevelController = FindObjectOfType<GC.LevelController>();
        }

        // Update is called once per frame
        void Update()
        {
            // If the game is in a state where the player can shoot (i.e. while playing)
            if (LevelController.gameState == GC.LevelController.GameState.playing)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    // Let the bullet spawner handle shooting
                    bulletSpawner.TryFire();
                }
            }
        }
    }
}

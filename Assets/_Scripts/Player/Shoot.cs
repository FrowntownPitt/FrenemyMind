using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Shoot : MonoBehaviour
    {
        public BulletSpawner bulletSpawner;

        public GC.GameController gameController;

        // Use this for initialization
        void Start()
        {
            gameController = FindObjectOfType<GC.GameController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (gameController.gameState == GC.GameController.GameState.playing)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    bulletSpawner.TryFire();
                }
            }
        }
    }
}

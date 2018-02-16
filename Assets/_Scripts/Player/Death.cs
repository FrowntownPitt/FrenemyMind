using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    // Player's Death controller
    public class Death : MonoBehaviour
    {

        public float deathTime = 5f; // How long the death animation takes
        public float scaleFactor = .9f; // How much the player's sprite is reduced each tick
        public float torque; // How fast to spin on death

        GC.LevelController LevelController;

        public void Start()
        {
            // Cache the level controller
            LevelController = FindObjectOfType<GC.LevelController>();
        }

        public void Die()
        {
            // Stop healing
            GetComponent<HealthController>().PauseHealing();
            // Tell the LevelController the player has died
            LevelController.Die();
            // Run the death animation
            StartCoroutine(DeathAnimation());
        }

        IEnumerator DeathAnimation()
        {
            gameObject.GetComponent<Rigidbody2D>().AddTorque(torque); // Spin the player (animation)

            float startTime = Time.time;
            while(Time.time - startTime < deathTime)
            {
                transform.localScale *= scaleFactor; // Shrink the player (animation)
                // Wait for next frame, yield to allow other threads to run
                yield return new WaitForEndOfFrame();
            }

            // Once the animation is over, remove the player from the scene
            Destroy(gameObject);

            // Tell the level controller to end the level
            LevelController.EndLevel();
        }
    }
}
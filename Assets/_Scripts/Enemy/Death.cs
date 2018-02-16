using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GC;

namespace Enemy
{
    // Enemy Death handler
    public class Death : MonoBehaviour
    {
        // Death animation parameters
        public float spinSpeed = 10f;
        public float scaleAmt = 0.99f;

        // How long the death lasts
        public float spinTime = 3f;
        

        //public GameObject ScoreControllerContainer;

        private ScoreController scoreController;
        private AudioController audioController;

        private bool isAlive = true;

        // Use this for initialization
        void Start()
        {
            //GameObject ScoreControllerContainer = GameObject.FindWithTag("LevelController");
            //Debug.Log(ScoreControllerContainer.name);
            scoreController = FindObjectOfType<ScoreController>();
            audioController = FindObjectOfType<AudioController>();
        }
        
        // Die() helper function
        public void Die()
        {
            // Pick a random direction to spin, run the internal Die()
            Die(Random.Range(-1f, 1f));
        }

        // Internal Die()
        void Die(float spinDirection)
        {
            // Play an explosion
            audioController.PlayAudio(AudioController.Sources.Explosion);

            // Start the death animation
            StartCoroutine(Spin(spinTime, spinDirection));
            //StartCoroutine(DisableCollider(1f));
        }

        IEnumerator Spin(float time, float spinDirection)
        {
            float startTime = Time.time;
            gameObject.GetComponent<Rigidbody2D>().AddTorque(spinSpeed*spinDirection);
            // run the scaling animation for the duration
            while (Time.time - startTime <= time)
            {
                gameObject.transform.localScale *= scaleAmt;
                //Debug.Log("Waiting before: " + (Time.time - startTime) + ". Time: " + time);
                // At 1/4 the animation time, remove the collider
                if(Time.time - startTime > time / 4)
                {
                    //GetComponent<Collider2D>().enabled = false;
                    GetComponent<CapsuleCollider2D>().size *= 0;
                }
                yield return new WaitForSeconds(0.01f);//WaitForEndOfFrame();
            }
            
            if (isAlive)
            {
                // Update the scoreboard with the enemy value
                scoreController.Destroyed((int)ScoreController.Types.enemy);
            }
            isAlive = false;

            // Tell the spawner this ship has died
            if(GetComponent<Movement>().isCounted)
                FindObjectOfType<EnemySpawner>().Died();
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("Entered trigger: " + other.name);
            // When the ship collides with a player bullet, die
            if (GetComponent<Movement>().isCounted && other.CompareTag("playerbullet"))
            {
                //Debug.Log("Die");
                Vector2 b = other.transform.position;
                Vector2 s = transform.position;

                // Calculate which way to rotate based on where the bullet hit the ship
                float f = Mathf.Tan(Mathf.Atan2(b.y - s.y, b.x - s.x));
                //Debug.Log("Angle: " + f);
                if (f == 0) f = 1f;
                Die(f/Mathf.Abs(f));

                // Destroy the player bullet
                Destroy(other.gameObject);
            }
        }
    }
}
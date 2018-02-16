using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    // Player's movement handler
    public class Movement : MonoBehaviour
    {
        GC.AudioController audioController;
        GC.LevelController LevelController;

        public float idleEngineLevel;
        public float activeEngineLevel;

        public float speed = 5f;
        private Rigidbody2D rb;

        private bool isPlaying = false;

        public float AxisDeadzone = 0.1f;

        // Use this for initialization
        void Start()
        {
            audioController = FindObjectOfType<GC.AudioController>();
            LevelController = FindObjectOfType<GC.LevelController>();
            rb = GetComponent<Rigidbody2D>();

            // Enable engine rumble 
            audioController.ToggleAudioContinuous(GC.AudioController.Sources.Movement, true);
            audioController.PlayAudio(GC.AudioController.Sources.Movement);
        }
        

        void FixedUpdate()
        {
            // Move only while permitted
            if (LevelController.gameState == GC.LevelController.GameState.playing)
            {
                float moveH = Input.GetAxis("Horizontal");
                float moveV = Input.GetAxis("Vertical");

                Vector3 motion = new Vector3(moveH, moveV, 0);

                rb.AddForce(motion * speed);

                if (!isPlaying && (Mathf.Abs(moveH) > AxisDeadzone || Mathf.Abs(moveV) > AxisDeadzone))
                {
                    // When moving, boost the engine audio
                    //audioController.PlayAudio(GC.AudioController.Sources.Movement);
                    audioController.SetAudioVolume(GC.AudioController.Sources.Movement, activeEngineLevel);
                    isPlaying = true;
                }
                else if (isPlaying && (Mathf.Abs(moveH) <= AxisDeadzone && Mathf.Abs(moveV) <= AxisDeadzone))
                {
                    // Once no longer moving, reduce the engine audio
                    //audioController.StopAudio(GC.AudioController.Sources.Movement);
                    audioController.SetAudioVolume(GC.AudioController.Sources.Movement, idleEngineLevel);
                    isPlaying = false;
                }
            }

            //gameObject.transform.position += motion * speed * Time.deltaTime;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            // On collision with enemy ship
            if (other.gameObject.CompareTag("enemy"))
            {
                Debug.Log("Collided with enemy!");
                // That ship must die
                other.GetComponent<Enemy.Death>().Die();
                // Update our health
                GetComponent<HealthController>().HitObject(HealthController.Types.enemy);
            }

            // On collision with an enemy bullet
            if (other.gameObject.CompareTag("bullet"))
            {
                Debug.Log("Collided with enemy bullet!");
                // That prefab must die
                other.GetComponent<Enemy.Death>().Die();
                // Update health
                GetComponent<HealthController>().HitObject(HealthController.Types.enemyBullet);
            }

            // On collision with asteroids
            if (other.gameObject.CompareTag("asteroid"))
            {
                Debug.Log("Collided with enemy asteroid!");
                // Kill the asteroid
                other.GetComponent<Enemy.Death>().Die();
                // Update our health
                GetComponent<HealthController>().HitObject(HealthController.Types.asteroid);
            }

            // When we hit a wall, bounce with 30% elasticity.
            if (other.gameObject.CompareTag("boundary"))
            {
                if (other.gameObject.name.Equals("top"))
                {
                    Vector2 s = rb.velocity;
                    s.x *= 1f;
                    s.y *= -0.3f;
                    rb.velocity = s;
                }
                if (other.gameObject.name.Equals("side"))
                {
                    Vector2 s = rb.velocity;
                    s.x *= -0.3f;
                    s.y *= 1f;
                    rb.velocity = s;
                }
            }
        }


    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
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

            audioController.ToggleAudioContinuous(GC.AudioController.Sources.Movement, true);
            audioController.PlayAudio(GC.AudioController.Sources.Movement);
        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    GetComponent<Collider2D>().enabled = !GetComponent<Collider2D>().enabled;
            //}
        }

        void FixedUpdate()
        {
            if (LevelController.gameState == GC.LevelController.GameState.playing)
            {
                float moveH = Input.GetAxis("Horizontal");
                float moveV = Input.GetAxis("Vertical");

                Vector3 motion = new Vector3(moveH, moveV, 0);

                rb.AddForce(motion * speed);

                if (!isPlaying && (Mathf.Abs(moveH) > AxisDeadzone || Mathf.Abs(moveV) > AxisDeadzone))
                {
                    //audioController.PlayAudio(GC.AudioController.Sources.Movement);
                    audioController.SetAudioVolume(GC.AudioController.Sources.Movement, activeEngineLevel);
                    isPlaying = true;
                }
                else if (isPlaying && (Mathf.Abs(moveH) <= AxisDeadzone && Mathf.Abs(moveV) <= AxisDeadzone))
                {
                    //audioController.StopAudio(GC.AudioController.Sources.Movement);
                    audioController.SetAudioVolume(GC.AudioController.Sources.Movement, idleEngineLevel);
                    isPlaying = false;
                }
            }

            //gameObject.transform.position += motion * speed * Time.deltaTime;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("enemy"))
            {
                Debug.Log("Collided with enemy!");
                other.GetComponent<Enemy.Death>().Die();
                GetComponent<HealthController>().HitObject(HealthController.Types.enemy);
            }

            if (other.gameObject.CompareTag("bullet"))
            {
                Debug.Log("Collided with enemy bullet!");
                other.GetComponent<Enemy.Death>().Die();
                GetComponent<HealthController>().HitObject(HealthController.Types.enemyBullet);
            }

            if (other.gameObject.CompareTag("asteroid"))
            {
                Debug.Log("Collided with enemy asteroid!");
                other.GetComponent<Enemy.Death>().Die();
                GetComponent<HealthController>().HitObject(HealthController.Types.asteroid);
            }

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
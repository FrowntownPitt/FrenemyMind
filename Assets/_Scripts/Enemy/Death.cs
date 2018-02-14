using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GC;

namespace Enemy
{
    public class Death : MonoBehaviour
    {

        public float spinSpeed = 10f;
        public float scaleAmt = 0.99f;

        public float spinTime = 3f;
        

        //public GameObject ScoreControllerContainer;

        private ScoreController scoreController;
        private AudioController audioController;

        private bool isAlive = true;

        // Use this for initialization
        void Start()
        {
            GameObject ScoreControllerContainer = GameObject.FindWithTag("GameController");
            //Debug.Log(ScoreControllerContainer.name);
            scoreController = ScoreControllerContainer.GetComponent<ScoreController>();
            audioController = ScoreControllerContainer.GetComponent<AudioController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Die()
        {
            Die(Random.Range(-1f, 1f));
        }

        void Die(float spinDirection)
        {
            audioController.PlayAudio(AudioController.Sources.Explosion);

            StartCoroutine(Spin(spinTime, spinDirection));
            //StartCoroutine(DisableCollider(1f));
        }

        IEnumerator Spin(float time, float spinDirection)
        {
            float startTime = Time.time;
            gameObject.GetComponent<Rigidbody2D>().AddTorque(spinSpeed*spinDirection);
            while (Time.time - startTime <= time)
            {
                gameObject.transform.localScale *= scaleAmt;
                //Debug.Log("Waiting before: " + (Time.time - startTime) + ". Time: " + time);
                if(Time.time - startTime > time / 4)
                {
                    //GetComponent<Collider2D>().enabled = false;
                    GetComponent<CapsuleCollider2D>().size *= 0;
                }
                yield return new WaitForSeconds(0.01f);//WaitForEndOfFrame();
            }
            if (isAlive)
            {
                scoreController.Destroyed((int)ScoreController.Types.enemy);
            }
            isAlive = false;

            if(GetComponent<Movement>().isCounted)
                FindObjectOfType<EnemySpawner>().Died();
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("Entered trigger: " + other.name);
            if (GetComponent<Movement>().isCounted && other.CompareTag("playerbullet"))
            {
                //Debug.Log("Die");
                Vector2 b = other.transform.position;
                Vector2 s = transform.position;

                float f = Mathf.Tan(Mathf.Atan2(b.y - s.y, b.x - s.x));
                //Debug.Log("Angle: " + f);
                if (f == 0) f = 1f;
                Die(f/Mathf.Abs(f));
                Destroy(other.gameObject);
            }
        }
    }
}
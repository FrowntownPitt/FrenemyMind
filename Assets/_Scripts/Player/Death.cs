using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Death : MonoBehaviour
    {
        public float deathTime = 5f;
        public float scaleFactor = .9f;
        public float torque;

        GC.LevelController LevelController;

        public void Start()
        {
            LevelController = FindObjectOfType<GC.LevelController>();
        }

        public void Die()
        {
            GetComponent<HealthController>().PauseHealing();
            LevelController.Die();
            StartCoroutine(DeathAnimation());
        }

        IEnumerator DeathAnimation()
        {
            gameObject.GetComponent<Rigidbody2D>().AddTorque(torque);

            float startTime = Time.time;
            while(Time.time - startTime < deathTime)
            {
                transform.localScale *= scaleFactor;
                yield return new WaitForEndOfFrame();
            }

            Destroy(gameObject);

            LevelController.EndLevel();
        }
    }
}
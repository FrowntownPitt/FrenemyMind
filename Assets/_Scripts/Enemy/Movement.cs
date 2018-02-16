using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    // Enemy's movement and collision
    public class Movement : MonoBehaviour
    {
        public Vector2 speed;
        public Vector2 speedRange;

        public float rotation;

        public bool isCounted = false;
        
        // Use this for initialization
        void Start()
        {
            // pick a random speed
            float sx = Random.Range(speed.x * (1 - speedRange.x), speed.x * (1 + speedRange.x));
            float sy = Random.Range(speed.y * (1 - speedRange.y), speed.y * (1 + speedRange.y));
            if(Random.Range(0,1) > 0.5)
            {
                sy *= -1;
            }
            speed = new Vector2(sx, sy);
            GetComponent<Rigidbody2D>().AddForce(speed);
            GetComponent<Rigidbody2D>().AddTorque(rotation);
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            // When the object leaves the screen, remove it and tell the controller it escaped
            if (collision.tag.Equals("window"))
            {
                Destroy(gameObject);
                if (isCounted)
                {
                    FindObjectOfType<GC.EnemySpawner>().Died();
                    FindObjectOfType<GC.LevelController>().EnemyEscaped();
                }
            }
        }
    }
}

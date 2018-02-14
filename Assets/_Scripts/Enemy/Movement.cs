using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Movement : MonoBehaviour
    {
        public Vector2 speed;
        public Vector2 speedRange;

        public bool isCounted = false;
        
        // Use this for initialization
        void Start()
        {
            float sx = Random.Range(speed.x * (1 - speedRange.x), speed.x * (1 + speedRange.x));
            float sy = Random.Range(speed.y * (1 - speedRange.y), speed.y * (1 + speedRange.y));
            if(Random.Range(0,1) > 0.5)
            {
                sy *= -1;
            }
            speed = new Vector2(sx, sy);
            GetComponent<Rigidbody2D>().AddForce(speed);
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag.Equals("window"))
            {
                Destroy(gameObject);
                if (isCounted)
                {
                    FindObjectOfType<GC.EnemySpawner>().Died();
                    FindObjectOfType<GC.GameController>().EnemyEscaped();
                }
            }
        }
    }
}

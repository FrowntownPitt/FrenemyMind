using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    // Bullet animation
    public class BulletAnimator : MonoBehaviour
    {

        private enum State
        {
            small,
            large
        }


        private State currentState = State.small;
        public GameObject bulletSprite;
        private Transform t;

        public float scaleAmt = 1.1f;
        public float scaleTime = 0.2f;

        // Use this for initialization
        void Start()
        {
            t = bulletSprite.GetComponent<Transform>();
            StartCoroutine(Animate());
        }
        
        // Make the bullet get bigger and smaller
        IEnumerator Animate()
        {
            while (true)
            {
                // Switch from big->small->big
                switch (currentState)
                {
                    case State.small:
                        {
                            currentState = State.large;
                            t.localScale *= scaleAmt;
                            break;
                        }
                    case State.large:
                        {
                            currentState = State.small;
                            t.localScale /= scaleAmt;
                            break;
                        }
                }
                // Wait for the animation duration
                yield return new WaitForSeconds(scaleTime);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            //Debug.Log(other.tag);
            // When the bullet leaves the window destroy it
            if (other.CompareTag("window"))
            {
                Destroy(gameObject);
                StopAllCoroutines();
            }
        }
    }
}
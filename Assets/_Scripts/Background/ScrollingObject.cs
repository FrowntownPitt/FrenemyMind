using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GC;

namespace Background
{
    public class ScrollingObject : MonoBehaviour
    {
        public GameController gameController;

        private Rigidbody2D rb;
        public float scrollSpeed = -1.25f;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(scrollSpeed, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (gameController != null && gameController.gameOver)
            {
                rb.velocity *= 0;
            }
        }
    }
}
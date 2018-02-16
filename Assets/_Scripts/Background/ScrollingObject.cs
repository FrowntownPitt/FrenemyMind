using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GC;

namespace Background
{
    // Background Scroller
    public class ScrollingObject : MonoBehaviour
    {
        private Rigidbody2D rb;
        public float scrollSpeed = -1.25f;
        
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(scrollSpeed, 0);
        }
    }
}
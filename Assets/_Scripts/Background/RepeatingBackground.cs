using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Background
{
    public class RepeatingBackground : MonoBehaviour
    {
        public GameObject backgroundRoot;
        //public GameObject backgroundNext;
        private float imageRoot;
        //private float imageNext;

        public float imageSizeX = 19.2f;

        void Start()
        {
            imageRoot = backgroundRoot.transform.position.x;
            //imageNext = backgroundNext.transform.position.x;
        }

        void Update()
        {
            if(transform.position.x < imageRoot-imageSizeX )
            {
                RepositionBackground();
            }
        }


        void RepositionBackground()
        {
            Vector2 offset = new Vector2(imageSizeX*2, 0f);
            transform.position = (Vector2)transform.position + offset;
        }
    }

}
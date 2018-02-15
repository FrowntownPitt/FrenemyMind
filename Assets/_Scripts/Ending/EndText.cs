using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ending
{
    public class EndText : MonoBehaviour
    {
        public Text text;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (FindObjectOfType<GC.GameController>().won)
            {
                text.text = "You have saved Earth!";
            } else
            {
                text.text = "The Earth is doomed.";
            }
        }
    }
}
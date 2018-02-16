using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ending
{
    // End scene text handler
    public class EndText : MonoBehaviour
    {
        public Text text;
        
        void Update()
        {
            // Update the end scene's text
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
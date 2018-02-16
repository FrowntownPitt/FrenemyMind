using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Buttons
{
    // End screen's button handler
    public class Reset : MonoBehaviour
    {

        public Button button;

        private void Start()
        {
            button.onClick.AddListener(OnClickListener);
        }

        // When this button is clicked, return to the main scene
        void OnClickListener()
        {
            // Load the scene as a single (reset the game controllers)
            SceneManager.LoadScene("Main");
        }

    }
}
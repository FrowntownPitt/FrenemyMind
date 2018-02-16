using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    // Play button's handler
    public class Begin : MonoBehaviour
    {

        public Button begin;

        private void Start()
        {
            begin.onClick.AddListener(OnClickListener);
        }

        void OnClickListener()
        {
            // Remove the start scene elements and load the Level1 Scene per GC.SceneLoader's policy
            GC.SceneLoader sceneLoader = FindObjectOfType<GC.SceneLoader>();

            sceneLoader.DisableSceneElements();
            sceneLoader.LoadScene("Level1");
        }

    }
}
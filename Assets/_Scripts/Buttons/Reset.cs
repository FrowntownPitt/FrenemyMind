using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Buttons
{
    public class Reset : MonoBehaviour
    {

        public Button button;

        private void Start()
        {
            button.onClick.AddListener(OnClickListener);
        }

        void OnClickListener()
        {
            SceneManager.LoadScene("Main");

            //GC.SceneLoader sceneLoader = FindObjectOfType<GC.SceneLoader>();


            //sceneLoader.DisableSceneElements();
            //sceneLoader.LoadScene("Level1");
        }

    }
}
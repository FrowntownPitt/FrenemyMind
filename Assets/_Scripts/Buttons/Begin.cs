using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class Begin : MonoBehaviour
    {

        public Button begin;

        private void Start()
        {
            begin.onClick.AddListener(OnClickListener);
        }

        void OnClickListener()
        {
            GC.SceneLoader sceneLoader = FindObjectOfType<GC.SceneLoader>();

            sceneLoader.DisableSceneElements();
            sceneLoader.LoadScene("Level1");
        }

    }
}
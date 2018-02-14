using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GC
{
    public class ScoreController : MonoBehaviour
    {

        public Text ScoreAmtText;

        public int scoreAmt;

        public enum Types
        {
            enemy,
            asteroid
        };

        public int[] ScoreVals = { 10, 2 };



        public void Destroyed(int enemyType)
        {
            scoreAmt += ScoreVals[enemyType];
            UpdateText();
        }

        public void UpdateText()
        {
            ScoreAmtText.text = "Score: " + scoreAmt;
        }
    }
}
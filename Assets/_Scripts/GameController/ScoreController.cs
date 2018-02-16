using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GC
{
    // The Score Manager
    public class ScoreController : MonoBehaviour
    {

        public Text ScoreAmtText;

        public int scoreAmt;

        // Scoring types
        public enum Types
        {
            enemy,
            asteroid
        };

        // Values of the given scoring types
        public int[] ScoreVals = { 10, 2 };


        // When the player destroys an object, increase the score by that object's amount
        public void Destroyed(int enemyType)
        {
            scoreAmt += ScoreVals[enemyType];
            UpdateText();
        }

        // Update the Scoreboard
        public void UpdateText()
        {
            ScoreAmtText.text = "Score: " + scoreAmt;
        }
    }
}
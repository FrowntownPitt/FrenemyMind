using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    // Player's Health Manager
    public class HealthController : MonoBehaviour
    {
        public Text healthText;

        public int healthMax;
        public int healthAmt;
        public int regenAmt;
        public float regenTick = 2f;

        private int regenAmtSave;

        // Types of objects this player can hit
        public enum Types
        {
            enemy,
            asteroid,
            enemyBullet
        };

        // Values of the collided objects to reduce HP by
        // (Corresponds to the Types enum indices)
        public int[] HealthVals = { 30, 60, 10 };

        // Use this for initialization
        void Start()
        {
            // Regen health, run it in the background
            StartCoroutine(HealthRegen());
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Tell this HealthController what object was hit
        public void HitObject(Types type)
        {
            // Reduce the health by what it hit
            SetHealth(healthAmt - HealthVals[(int)type]);
            if(healthAmt <= 0)
            {
                // Tell the LevelController that the player has died
                FindObjectOfType<GC.LevelController>().Die();
                // Run the Die procedure for the player
                GetComponent<Death>().Die();
            }
        }
        
        public void PauseHealing()
        {
            regenAmtSave = regenAmt;    // Save the regen amount for when we resume later
            regenAmt = 0;
        }
        
        public void ResumeHealing()
        {
            regenAmt = regenAmtSave;
        }

        IEnumerator HealthRegen()
        {
            while (true)
            {
                // Run every <regenTick> seconds, regen by the set amount
                yield return new WaitForSeconds(regenTick);
                SetHealth(healthAmt + regenAmt);
            }
        }

        // Set the health, remaining in bounds, and update the text
        void SetHealth(int hp)
        {
            healthAmt = hp;
            if (healthAmt > healthMax)
                healthAmt = healthMax;
            if(healthAmt < 0)
                healthAmt = 0;
            
            healthText.text = "Hull: " + healthAmt;
        }
    }
}
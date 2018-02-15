using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class HealthController : MonoBehaviour
    {
        public Text healthText;

        public int healthMax;
        public int healthAmt;
        public int regenAmt;
        public float regenTick = 2f;

        private int regenAmtSave;

        public enum Types
        {
            enemy,
            asteroid,
            enemyBullet
        };

        public int[] HealthVals = { 30, 60, 10 };

        // Use this for initialization
        void Start()
        {
            StartCoroutine(HealthRegen());
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HitObject(Types type)
        {
            SetHealth(healthAmt - HealthVals[(int)type]);
            if(healthAmt <= 0)
            {
                FindObjectOfType<GC.LevelController>().Die();
                GetComponent<Death>().Die();
            }
        }

        public void PauseHealing()
        {
            regenAmtSave = regenAmt;
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
                yield return new WaitForSeconds(regenTick);
                SetHealth(healthAmt + regenAmt);
            }
        }

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
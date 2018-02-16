using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    // Handle spawning bullets for the player
    public class BulletSpawner : MonoBehaviour
    {
        GC.AudioController audioController;

        // Location to spawn the bullets
        public Transform spawner;
        
        public GameObject bulletPrefab;

        public Vector2 BulletSpeed;

        // values to handle shooting at at least a fixed rate
        private float previousFire;
        public float fireRate;

        public Text ammoText;
        public int ammoAmt; // How much ammo the player has

        [SerializeField]
        // Move the bullet at some percentage of the player's velocity
        private float bulletSpeedTweak = 5f;
        
        void Start()
        {
            // Cache the AudioController
            audioController = FindObjectOfType<GC.AudioController>();
            // Update the ammo text
            SetAmmoText(ammoAmt);
        }

        // Attempt firing
        public void TryFire()
        {
            // Do not allow firing if the fire period has not ended
            if (Time.time - previousFire >= fireRate)
            {
                // If there is ammo available, fire.
                if (ammoAmt > 0)
                    Fire();
                else
                {
                    // otherwise, play the misfire sound
                    StartCoroutine(MisfireEffect());
                }
            }
        }

        IEnumerator MisfireEffect()
        {
            // I had an interesting effect when playing the "click" sound
            // It was based on it playing many times rapidly
            // Play it in the background so nothing stutters
            for (int i = 0; i < 20; i++)
            {
                audioController.PlayAudio(GC.AudioController.Sources.Misfire);
                yield return new WaitForSeconds(Time.fixedDeltaTime/2);
            }
        }

        void Fire()
        {
            previousFire = Time.time;
            // Instantiate the bullet at the proper location in the level's scene (since they are added additively)
            GameObject bullet = Instantiate(bulletPrefab, spawner.position, spawner.rotation, GameObject.Find("InstantiateContainer").transform) as GameObject;
            // Give the bullet the proper speed based on the player's current velocity
            bullet.GetComponent<Rigidbody2D>().AddForce(BulletSpeed + bulletSpeedTweak * gameObject.GetComponent<Rigidbody2D>().velocity);
            SetAmmoText(--ammoAmt);

            audioController.PlayAudio(GC.AudioController.Sources.PlayerShooter);
        }

        void SetAmmoText(int ammo)
        {
            // Update the ammo text.
            ammoText.text = "Ammo: " + ammo;
        }
    }
}
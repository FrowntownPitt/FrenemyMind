using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class BulletSpawner : MonoBehaviour
    {
        GC.AudioController audioController;

        public Transform spawner;

        public GameObject bulletPrefab;

        public Vector2 BulletSpeed;

        private float previousFire;
        public float fireRate;

        public Text ammoText;
        public int ammoAmt;

        [SerializeField]
        private float bulletSpeedTweak = 5f;

        // Use this for initialization
        void Start()
        {
            audioController = FindObjectOfType<GC.AudioController>();
            SetAmmoText(ammoAmt);
        }

        public void TryFire()
        {
            if (Time.time - previousFire >= fireRate)
            {
                if (ammoAmt > 0)
                    Fire();
                else
                {
                    previousFire = Time.time;
                    StartCoroutine(MisfireEffect());
                }
            }
        }

        IEnumerator MisfireEffect()
        {

            for (int i = 0; i < 20; i++)
            {
                audioController.PlayAudio(GC.AudioController.Sources.Misfire);
                yield return new WaitForSeconds(Time.fixedDeltaTime/2);
            }
        }

        void Fire()
        {
            previousFire = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, spawner.position, spawner.rotation) as GameObject;
            //Debug.Log("Current velocity: " + GetComponent<Rigidbody2D>().velocity);
            bullet.GetComponent<Rigidbody2D>().AddForce(BulletSpeed + bulletSpeedTweak * gameObject.GetComponent<Rigidbody2D>().velocity);
            SetAmmoText(--ammoAmt);

            audioController.PlayAudio(GC.AudioController.Sources.PlayerShooter);
        }

        void SetAmmoText(int ammo)
        {
            ammoText.text = "Ammo: " + ammo;
        }
    }
}
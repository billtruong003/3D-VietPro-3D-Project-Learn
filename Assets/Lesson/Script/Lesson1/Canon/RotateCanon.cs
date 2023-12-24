using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace GunLesson
{
    public class RotateCanon : MonoBehaviour
    {
        [Header("Refference")]
        [SerializeField] private Transform target;
        [SerializeField] private Transform shootingPoint;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform bulletContainer;
        [SerializeField] private ParticleSystem explosionFX;
        [SerializeField] private AudioSource shootSFX;
        [Header("Assign Value")]
        [Range(0, 30)]
        [SerializeField] private float rotateSpeed;
        [SerializeField] private GameObject[] bulletToShoot = new GameObject[3];
        [SerializeField] private List<GameObject> bullets;
        private bool rotate = true;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Start");
            StartCoroutine(Cor_Shoot());
        }

        // Update is called once per frame
        void Update()
        {
            Rotate();
        }

        private void TriggerSFX()
        {
            shootSFX.PlayOneShot(shootSFX.clip);
        }
        [Button]
        private void Rotate()
        {
            if (rotate)
                transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        private void Explo()
        {
            Debug.Log($"{transform.parent.gameObject.name} Particle trigger");
            explosionFX.Stop();
            explosionFX.Play();
            TriggerSFX();
        }

        private IEnumerator Cor_Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                rotate = false;
                for (int i = 0; i < 3; i++)
                {
                    Shoot(i);
                    yield return new WaitForSeconds(0.3f);
                }
                rotate = true;
                yield return null;
            }
        }

        private void Shoot(int index)
        {
            if (bullet == null)
                return;
            bool boolReady = CheckBullet();
            if (boolReady)
            {
                Transform getBullet = bulletToShoot[index].transform;
                getBullet.gameObject.SetActive(true);
                getBullet.SetPositionAndRotation(spawnPoint.position, Quaternion.identity);
                getBullet.LookAt(shootingPoint);
                getBullet.GetComponent<CanonBullet>().Cor_Destroy();
                Explo();
            }
            else
            {
                GameObject bulletShoot = Instantiate(bullet, spawnPoint.position, Quaternion.identity, bulletContainer);
                bulletShoot.transform.LookAt(shootingPoint);
                bullets.Add(bulletShoot);
                Explo();
            }
        }
        private bool CheckBullet()
        {
            int countBullet = 0;
            if (bullets.Count != 0)
            {
                foreach (GameObject bull in bullets)
                {
                    if (!bull.activeSelf)
                    {
                        bulletToShoot[countBullet] = bull;
                        countBullet++;
                        if (countBullet == 3) return true;
                    }
                }
            }
            return false;
        }
    }
}
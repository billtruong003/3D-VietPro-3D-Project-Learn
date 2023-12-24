using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;


namespace GunLesson
{
    public class Gun : MonoBehaviour, IShootable
    {
        [Header("Refference Bullet")]
        [SerializeField] private protected Transform bulletContainer;
        [SerializeField] private protected GameObject bullet;
        [SerializeField] private protected List<GameObject> bullets;
        [SerializeField] private protected Queue<GameObject> qBullets = new();
        [SerializeField] private protected GameObject bulletToShoot;

        [Header("Refference Point")]
        [SerializeField] private protected Transform spawnPoint;
        [SerializeField] private protected Transform shootingPoint;

        [Header("Refference Sound")]
        [SerializeField] private protected AudioClip shootSfx;
        [SerializeField] private protected AudioClip reloadSfx;
        [SerializeField] private protected AudioSource audioSource;

        [Header("Refference FX")]
        [SerializeField] private protected ParticleSystem shoot_VFX;

        [Header("Refference Animator")]
        [SerializeField] private protected Animator anim;
        [SerializeField] private protected string shootTrigger = "Shoot";
        [SerializeField] private protected string reloadTrigger = "Reload";

        [Header("Assign Value")]
        [SerializeField] private protected string gunName;
        [SerializeField] private protected float maxAmmo;
        [SerializeField] private protected float ammoLeft;
        [SerializeField] private protected float damage;

        [Header("Selection")]
        [SerializeField] private protected bool useCor;

        private protected bool reloading;
        [Button]
        public virtual void Shoot()
        {
            if (Input.GetMouseButtonDown(0) && ammoLeft > 0)
            {
                if (anim != null)
                {
                    anim.SetTrigger("Shoot");
                }
                else
                {
                    Debug.Log("gunAnimator is not assigned in the inspector!");
                }
                Debug.Log("Shootsfx:" + shootSfx.name);
                Debug.Log("audioSource:" + audioSource.transform.name);
                audioSource.PlayOneShot(shootSfx);
                ammoLeft--;
                ShootVFX();

                if (!useCor)
                {
                    QueueShoot();
                }
                else
                {
                    if (CheckBulletReady())
                        PoolBullet();
                    else
                        SpawnBullet();
                }

            }
            else if (Input.GetMouseButtonDown(0) && ammoLeft <= 0)
            {
                anim.SetTrigger("Shoot");
                Debug.Log($" {gunName} Out of Ammo! Please Reload!");
            }
        }
        private protected void InitBullet()
        {

            for (int i = 0; i < maxAmmo; i++)
            {
                GameObject ammoSpawn = Instantiate(bullet, bulletContainer);
                ammoSpawn.SetActive(false);
                qBullets.Enqueue(ammoSpawn);
            }
        }

        private protected void TriggerBulletDestroy(GameObject objectToUse)
        {
            CanonBullet canonBull = objectToUse.GetComponent<CanonBullet>();
            if (canonBull != null)
                canonBull.Cor_Destroy();
        }

        private protected void QueueShoot()
        {
            bulletToShoot = qBullets.Dequeue();
            bulletToShoot.SetActive(true);
            Transform bulletPeek = bulletToShoot.transform;
            bulletPeek.position = spawnPoint.position;
            bulletPeek.LookAt(shootingPoint);
            TriggerBulletDestroy(bulletToShoot);
            qBullets.Enqueue(bulletToShoot);
        }

        private protected void SpawnBullet()
        {
            GameObject bulletSpawn = Instantiate(bullet, spawnPoint.position, Quaternion.identity, bulletContainer);
            bulletSpawn.transform.LookAt(shootingPoint);
            TriggerBulletDestroy(bulletSpawn);
            bullets.Add(bulletSpawn);
        }

        private protected void PoolBullet()
        {
            bulletToShoot.SetActive(true);
            TriggerBulletDestroy(bulletToShoot);
            Transform bulletUse = bulletToShoot.transform;
            bulletUse.position = spawnPoint.position;
            bulletUse.LookAt(shootingPoint);

        }

        private protected bool CheckBulletReady()
        {
            foreach (GameObject bullet in bullets)
            {
                if (!bullet.activeSelf)
                {
                    bulletToShoot = bullet;
                    return true;
                }
            }
            return false;
        }

        public virtual void Cor_Reload(float time)
        {

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (ammoLeft == maxAmmo || reloading == true)
                    return;

                StartCoroutine(Reload(time));
            }
        }

        private protected void ShootVFX()
        {
            shoot_VFX.Stop();
            shoot_VFX.Play();
        }

        private protected IEnumerator Reload(float waitTime)
        {
            reloading = true;
            audioSource.PlayOneShot(reloadSfx);
            anim.SetTrigger("Reload");
            yield return new WaitForSeconds(waitTime);
            ammoLeft = maxAmmo;
            reloading = false;
        }
    }
}

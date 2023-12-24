using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace GunLesson
{
    public class CanonBullet : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [Range(10, 100)]
        [SerializeField] private float shootSpeed;
        [SerializeField] private bool useCor;
        [SerializeField] private bool usePool;

        private float timeDestroy = 2;

        // Start is called before the first frame update
        void Start()
        {
            timeDestroy = Time.time + 1;
            if (useCor)
            {
                Cor_Destroy();
            }
        }

        private void Update()
        {
            Shoot();
        }

        public void Cor_Destroy()
        {
            StartCoroutine(DestroyBullet());
        }

        private void Shoot()
        {
            transform.Translate(Vector3.forward * shootSpeed * Time.deltaTime);
        }

        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(timeDestroy);
            if (!usePool)
            {
                Destroy(this.gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        [Button]
        private void LookAtTarget()
        {
            if (target != null)
                transform.LookAt(target);
        }

    }
}
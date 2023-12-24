using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace GunLesson
{
    public class Pistol : Gun
    {
        private void Start()
        {
            InitBullet();
        }
        private void Update()
        {
            Shoot();
            Cor_Reload(2);
        }

        public override void Cor_Reload(float time)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (ammoLeft == maxAmmo || reloading == true)
                    return;

                StartCoroutine(Reload(time));
            }
        }


    }
}
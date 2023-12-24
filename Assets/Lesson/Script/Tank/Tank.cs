using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

namespace TankBehaviour
{
    public class Tank : MonoBehaviour
    {
        [Header("Animator")]
        [SerializeField] private protected Animator tankAnim;

        [Header("Assign Value")]
        [SerializeField] private protected float moveSpeed;
        [SerializeField] private protected float rotateSpeed;

        [Header("Refference Setup")]
        [SerializeField] private protected TankRay tankRay;
        [SerializeField] private protected TankStatus tankData;
        [SerializeField] private protected Vector3 posTarget;

        private protected quaternion currentDir;
        private protected float limNev = -4;
        private protected float limPos = 4;
        private protected float distance;

        [Button]
        private protected void Setup()
        {
            GetTankRay();
        }

        private protected void GetTankRay()
        {
            tankRay = gameObject.GetComponentInChildren<TankRay>();
        }

        private protected void SlowlyRotate()
        {
            Vector3 dir = posTarget - transform.position;
            float check = Vector3.Angle(dir, transform.forward);
            Quaternion angleToLook = GetAngleLookAt(dir);
            transform.rotation = angleToLook;
        }

        private Quaternion GetAngleLookAt(Vector3 dir)
        {
            Quaternion lookAtDir = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
            lookAtDir.x = 0;
            lookAtDir.z = 0;

            currentDir = lookAtDir;
            return lookAtDir;
        }


    }
}
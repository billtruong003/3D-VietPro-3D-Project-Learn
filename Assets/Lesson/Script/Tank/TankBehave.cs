using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace RotateNorm
{
    public class TankBehave : MonoBehaviour
    {
        [Header("Anim")]
        [SerializeField] private Animator anim;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Ray ray;
        [SerializeField] private Collider sphereCol;

        // Corodinate
        private int xCor;
        private int zCor;
        private float distance;
        private bool movable;
        private Vector3 posTarget;
        private Quaternion currentDir;


        // Start is called before the first frame update
        void Start()
        {
            movable = true;
            Cor_TankMove();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void Cor_TankMove()
        {
            StartCoroutine(AI_Move());
        }
        private IEnumerator AI_Move()
        {
            posTarget = RandomPosition();
            yield return new WaitForSeconds(1);
            while (true)
            {
                if (!movable)
                {
                    TankMoveBack();
                    posTarget = RandomPosition();
                    movable = true;
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    tankMove();
                    checkDistance();
                    if (!movable)
                        continue;
                    if (distance < 6.1f)
                    {
                        posTarget = RandomPosition();
                        yield return new WaitForSeconds(1);
                    }
                }
                yield return null;
            }
        }

        private Vector3 RandomPosition()
        {
            xCor = Random.Range(-7, 7);
            zCor = Random.Range(-7, 7);
            return new Vector3(xCor, zCor);
        }

        private void checkDistance()
        {
            distance = Vector3.Distance(posTarget, transform.position);
            Debug.Log("Distance to other: " + distance);
            Debug.Log("Target Position: " + posTarget);
            Debug.Log("Object Position: " + transform.position);
        }

        [Button]
        private IEnumerator TankMoveBack()
        {
            for (int i = 0; i < 100; i++)
            {
                transform.Translate(Vector3.back * 5 * Time.deltaTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
        private void tankMove()
        {
            SlowlyRotate();
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        private void SlowlyRotate()
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

        private void SetMoveAnim(bool status)
        {
            anim.SetBool("Move", status);
        }
        private void InitRay()
        {
            ray = new Ray(transform.position, transform.forward);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, transform.forward);
        }

        private void OnTriggerEnter(Collider collide)
        {
            if (collide.gameObject.layer == 3)
                movable = false;
            LayerMask layer = 3;

            Debug.Log($"movable: {movable} |\n Layer: {layer.value}");
        }
    }
}
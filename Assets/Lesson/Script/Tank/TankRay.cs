using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBehaviour
{
    public class TankRay : MonoBehaviour
    {
        [SerializeField] private Transform[] pointRay;
        [SerializeField] private Ray[] ray;
        [SerializeField] private Collider sphereCol;
        [SerializeField] private LayerMask layerCol = 3;
        private bool collideBool = false;

        private Ray rayCs1;
        private Ray rayCs2;
        private Ray rayCs3;
        public bool GetBoolStatus => collideBool;
        // Update is called once per frame
        void Update()
        {
            castRay();
        }
        private void OnTriggerEnter(Collider collide)
        {
            if (collide.gameObject.layer == 3)
                collideBool = true;
            Debug.Log($"collideBool: {collideBool} |\n Layer: {1 << 3}");
        }
        private void castRay()
        {
            rayCs1 = new Ray(transform.position, pointRay[0].forward);
            rayCs2 = new Ray(transform.position, pointRay[1].forward);
            rayCs3 = new Ray(transform.position, pointRay[2].forward);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayCs1);
            Gizmos.DrawRay(rayCs2);
            Gizmos.DrawRay(rayCs3);

        }
        public caseCheck checkCollide()
        {
            bool rayCol1 = Physics.Raycast(rayCs1, 3, layerCol);
            bool rayCol2 = Physics.Raycast(rayCs2, 3, layerCol);
            bool rayCol3 = Physics.Raycast(rayCs3, 3, layerCol);
            if (rayCol1 && rayCol2 && rayCol3)
                return caseCheck.CASE1;
            else if (rayCol1 && rayCol2)
                return caseCheck.CASE2;
            else if (rayCol1 && rayCol3)
                return caseCheck.CASE3;
            else
            {
                if (rayCol1)
                {
                    return caseCheck.CASE4;
                }
                else if (rayCol2)
                {
                    return caseCheck.CASE5;
                }
                else if (rayCol3)
                {
                    return caseCheck.CASE6;
                }
            }
            return caseCheck.NONE;
        }

    }
    public enum caseCheck
    {
        NONE,
        CASE1,
        CASE2,
        CASE3,
        CASE4,
        CASE5,
        CASE6,
    }
}
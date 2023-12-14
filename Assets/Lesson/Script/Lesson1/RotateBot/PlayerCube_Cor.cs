using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson1
{
    public class PlayerCube_Cor : MonoBehaviour
    {
        [Header("Assign Value")]
        [Range(5, 10)]
        [SerializeField] private float speed;
        [Range(0, 5)]
        [SerializeField] private float rotateSpeed;
        [SerializeField] private int currentGoal;

        [Header("Time")]
        [SerializeField] private float timing = 2;

        [Header("Points")]
        [SerializeField] private Transform[] points;

        [Header("Debug")]
        [NaughtyAttributes.ReadOnly] public float DebugGoal;

        // Private
        private float distanceLim = 1f;
        private int pointLength;
        private bool reachGoal = false;
        private Quaternion currentDir;
        private Vector3 currentAngle;
        private Vector3 goal;
        private bool rightAngle = false;
        // Start is called before the first frame update
        void Start()
        {
            pointLength = points.Length;
            SetGoal();
            StartCoroutine(AutoMove());
        }
        private IEnumerator AutoMove()
        {
            yield return new WaitForSeconds(timing);
            while (true)
            {
                if (!rightAngle)
                {
                    RotateToPoint(points[currentGoal]);
                    yield return null;
                    continue;
                }
                Movement();
                float distanceCheck = Vector3.Distance(goal, transform.position);
                Debug.Log($"Distance: {distanceCheck}");
                if (distanceCheck < distanceLim)
                {
                    if (currentGoal == pointLength - 1)
                    {
                        currentGoal = 0;
                    }
                    else
                    {
                        currentGoal++;
                    }
                    SetGoal();
                    rightAngle = false;
                    yield return new WaitForSeconds(2);
                    continue;
                }
                yield return null;
            }
        }
        private void Movement()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        private void SetGoal()
        {
            goal = points[currentGoal].position;
        }
        private void RotateToPoint(Transform point)
        {
            // Vector3.ang
            Vector3 dir = point.position - transform.position;
            float check = Vector3.Angle(dir, transform.forward);
            Debug.Log($"Check: {check}");
            if (check < 3f)
            {
                Debug.Log("Correct");
                rightAngle = true;
                return;
            }
            Debug.Log("Scrolling");
            Quaternion angleToLook = GetAngleLookAt(point, dir);
            transform.rotation = angleToLook;
        }
        private Quaternion GetAngleLookAt(Transform target, Vector3 dir)
        {
            // hàm slerp nhằm lấy góc xen giữa, (vector1, vector2, volume xoay mỗi lần)
            // ví dụ rotate speed đặt cao thì sẽ càng xoay nhanh hơn vì lúc đó volume tăng lên
            // Nếu góc đó cách 90 thì khi volume set 10 đơn thuần mà không nhân cho deltatime thì nó sẽ chỉ cần gọi hàm này 9 lần là đạt yêu cầu
            Quaternion lookAtDir = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
            lookAtDir.x = 0;
            lookAtDir.z = 0;

            currentDir = lookAtDir;
            return lookAtDir;
        }
        public void SetPoints(Transform[] arrPoints)
        {
            distanceLim = 3f;
            timing = Time.time + 2;
            pointLength = arrPoints.Length;
            points = new Transform[pointLength];
            points = arrPoints;

            SetGoal();
        }
    }
}
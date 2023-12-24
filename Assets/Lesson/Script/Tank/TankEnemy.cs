using System.Collections;
using NaughtyAttributes;
using TankBehaviour;
using UnityEngine;

public class TankEnemy : Tank
{
    [SerializeField] private float xCor;
    [SerializeField] private float zCor;
    [SerializeField] private bool useCor;
    [SerializeField] private Transform point;
    [SerializeField] private Transform pointCheck;
    private float angleCheck;
    private bool rangeCheck;

    void Start()
    {
        RandomPosTarget();
    }

    void Update()
    {
        checkDistance();
    }
    [Button]
    private void CheckDistance()
    {
        distance = Vector3.Distance(point.position, pointCheck.position);
        Debug.Log($"distance: {distance}");
    }
    private void RandomPosTarget()
    {
        xCor = Random.Range(limNev, limPos);
        zCor = Random.Range(limNev, limPos);
        posTarget = new Vector3(xCor, 0, zCor);
        StartCoroutine(Cor_Move());
    }
    private void checkDistance()
    {
        distance = Vector3.Distance(transform.position, posTarget);
        Debug.Log(distance);
    }

    private IEnumerator Cor_Move()
    {
        yield return new WaitForSeconds(2);
        SetMoveAnim(true);
        while (distance > 1f)
        {
            tankMove();
            if (tankRay.GetBoolStatus)
            {
                caseCheck getCase = tankRay.checkCollide();
                Debug.Log("Case:" + getCase.ToString());
                TurnCase(getCase);
                yield break;
            }
            yield return null;
        }
        SetMoveAnim(false);
        RandomPosTarget();
    }
    private void tankMove()
    {
        SlowlyRotate();
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    private void SetMoveAnim(bool status)
    {
        tankAnim.SetBool("Move", status);
    }

    // Case1 pick random right or left turn and pick another position
    // Case2 turn right and pick another position
    // Case3 turn left and pick another position
    // Case4 turn random right or left and pick another position
    // Case5 turn right and pick another position
    // Case6 turn left and pick another position

    private void TurnCase(caseCheck conditionCase)
    {
        if (conditionCase == caseCheck.NONE)
            return;

        if (conditionCase == caseCheck.CASE1)
        {
            TurnRandomRightLeft();
        }
        else if (conditionCase == caseCheck.CASE2)
        {
            TurnRandomRightLeft();
        }
        else if (conditionCase == caseCheck.CASE3)
        {
            TurnRandomRightLeft();
        }
        else if (conditionCase == caseCheck.CASE4)
        {
            TurnRandomRightLeft();
        }
        else if (conditionCase == caseCheck.CASE5)
        {
            TurnRandomRightLeft();
        }
        else if (conditionCase == caseCheck.CASE6)
        {
            TurnRandomRightLeft();
        }
    }
    private IEnumerator TurnRandomRightLeft()
    {
        PickRandomDirection();
        while (angleCheck > 5)
        {
            Vector3 dir = posTarget - transform.position;
            angleCheck = Vector3.Angle(dir, transform.forward);
            SlowlyRotate();
            yield return null;
        }
        RandomPosTarget();

    }
    private void PickRandomDirection()
    {
        int randomNum = Random.Range(0, 2);
        if (randomNum != 1)
        {
            posTarget = new Vector3(0, 90, 0);
            return;
        }
        posTarget = new Vector3(0, -90, 0);
    }
}

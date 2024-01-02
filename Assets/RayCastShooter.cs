using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShooter : MonoBehaviour
{
    private int RaysToShoot = 30;

    private void ShootRay()
    {
        float angle = 0;
        for (int i = 0; i < RaysToShoot; i++)
        {
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);
            angle += 2 * Mathf.PI / RaysToShoot;

            Vector3 dir = new Vector3(transform.position.x + x, transform.position.y + y, 0);
            RaycastHit hit;
            Debug.DrawLine(transform.position, dir, Color.red);
            if (Physics.Raycast(transform.position, dir, out hit))
            {
                //here is how to do your cool stuff ;)
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ShootRay();
    }



}

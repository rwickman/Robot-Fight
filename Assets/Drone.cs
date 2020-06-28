using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public float stoppingDistance;
    public float speed;
    public float rotationSpeed;

    public float maxFloatDistance;
    public float lerpFloatPct;

    // The center of the Drone that does not account for floatting
    private Vector3 centerPosition;
    private Vector3 curFloat;
    private bool floatAscend = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateFloat();
        Move(new Vector3(0, 50f, 40f));

        print(curFloat);
        //transform.position = centerPosition + curFloat;
    }

    public void Move(Vector3 target)
    {
        if (Vector3.Distance(target, transform.position) > stoppingDistance)
        {
            centerPosition = Vector3.MoveTowards(centerPosition, target, Time.deltaTime * speed);
            transform.position = centerPosition + curFloat;
            UpdateRotation(target);
        }
    }

    public void UpdateRotation(Vector3 target)
    { 
        Vector3 rotDir = (target - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(rotDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime );
    }

    public void UpdateFloat()
    {
        if (curFloat.y >= maxFloatDistance)
        {
            floatAscend = false;
            print("UPDATING FLOAT ASCEND");
        }
        else if (curFloat.y <= -maxFloatDistance) {
            floatAscend = true;
        }
        curFloat.y += (floatAscend ? maxFloatDistance : -maxFloatDistance) * lerpFloatPct * Time.deltaTime;
    }
}

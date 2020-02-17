using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipforward : MonoBehaviour
{
    //movement speed in units per second
    public float movementspeed = 2;

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 localVectorUp = transform.InverseTransformDirection(0, 0, 1);

        pos += localVectorUp * movementspeed * Time.deltaTime;
        transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectScript : MonoBehaviour
{
    //Basic script to rotate an object.

    // Update is called once per frame
    void Update()
    {
        float angle = 50 * Time.deltaTime;
        transform.Rotate(0,angle,0);
    }
}

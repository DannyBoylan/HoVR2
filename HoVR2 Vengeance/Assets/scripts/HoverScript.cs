using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour
{
    public bool direction = true;
    [Range(0.1f, 10)]
    public float speed;
    public float position = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float speedtime = speed * Time.deltaTime;
        if (direction)
        {
            if (position >= 15)
            {
                direction = false;

            }
            else if (position >= 10)
            {
                transform.position += new Vector3(+0, (speedtime / 8), +0);
                position++;
            }
            else if (position >= 5)
            {
                transform.position += new Vector3(+0, (speedtime / 4), +0);
                position++;
            }
            else if (position <= 5)
            {
                transform.position += new Vector3(+0, speedtime, +0);
                position++;
            }            
        }
        else
        {
            if (position <= -15)
            {
                direction = true;

            }
            else if (position <= -10)
            {
                transform.position -= new Vector3(+0, (speedtime / 8), +0);
                position--;
            }
            else if (position <= -5)
            {
                transform.position -= new Vector3(+0, (speedtime / 4), +0);
                position--;
            }
            else if (position >= -5)
            {
                transform.position -= new Vector3(+0, speedtime, +0);
                position--;
            }
        }
    }
}

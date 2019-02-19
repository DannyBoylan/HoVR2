using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretControl : MonoBehaviour
{
    public GameObject closestEnemy,player;
    public float acceleration = 15f;
    private float speed;
    public float maxSpeed = 70.0f;
    public int startDelay = 100;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("playerController");
    }

    // Update is called once per frame
    void Update()
    {
        RotationalScript();
    }

    void RotationalScript()
    {


        if (startDelay > 0)
        {
            // Start countdown
            startDelay -= 1;

        }
        else
        {

            //grab the closest enemy from the proximity detection script
            closestEnemy = player.GetComponent<ProximityDetection>().closestEnemy;

            //This makes sure that there is an enemy that is considered close so make sure the turret script goes in this if statement.
            if (closestEnemy != null)
            {


                Quaternion rotation = Quaternion.LookRotation(closestEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
                if (speed < maxSpeed)
                {
                    speed += Time.deltaTime * acceleration;
                }
                if (Math.DotProduct(closestEnemy.transform.position - transform.position, transform.forward) > 0.999f)
                {
                    speed = 0;
                    transform.LookAt(closestEnemy.transform.position);

                }

            }


        }



    }






}

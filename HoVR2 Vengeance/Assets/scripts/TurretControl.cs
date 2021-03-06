﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour {
    public GameObject closestEnemy;

    public float acceleration = 0.01f;
    public float speed;
    public float maxSpeed = 10.0f;


    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
        //grab the closest enemy from the proximity detection script
        closestEnemy = GameObject.FindGameObjectWithTag("Player").GetComponent<ProximityDetection>().closestEnemy;




        //This makes sure that there is an enemy that is considered close so make sure the turret script goes in this if statement.
        if (closestEnemy != null)
        {
            Quaternion rotation = Quaternion.LookRotation(closestEnemy.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
            if (speed < maxSpeed)
            {
                speed += Time.deltaTime* acceleration;
                
            }
            if(Math.DotProduct(closestEnemy.transform.position - transform.position, transform.forward) > 0.999f)
            {
                speed = 0;
                transform.LookAt(closestEnemy.transform.position);
            }
            else
            {

            }
        }

	}
}

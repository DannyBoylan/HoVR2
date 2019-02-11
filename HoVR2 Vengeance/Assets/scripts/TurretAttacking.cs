//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TurretAttacking : MonoBehaviour {
    // Use this for initialization
    /*void Start () {	}*/

    public float PDDroneDamage = 10f;
    public float PDDroneRange = 100000f;

    // this is so can attach shooting to camera for fps, change to turret hole later
   // public GameObject Emitter;

    void Shoot()
    {
        //creates a ray called hit
        RaycastHit hit;
        
        // this actually shoots out the ray. first variable is where the ray shoots from.  2nd is the direction (forward). 3nd gathers info and puts it in hit, 4th is the range.
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit))
        {
           // Debug.Log(hit.transform.name);
            

            Enemy target = hit.transform.GetComponent<Enemy>();

            if (target != null)
            {
                //This sends the damage variable of the drone
                target.TakeDamage(PDDroneDamage);
            }

        }

    }


    // Update is called once per frame
    void Update ()
    {
        // putting shoot on fire button just for testing
        if (Input.GetKeyDown("/"))
        {
            Shoot();

        }
        //Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);

    }










}

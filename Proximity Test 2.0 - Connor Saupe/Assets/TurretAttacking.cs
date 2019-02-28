using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttacking : MonoBehaviour
{
    // Use this for initialization

    public float PDDroneDamage = 10f;
    public float PDDroneRange = 10000f;
    public int fireRate = 180;

    // this is so can attach shooting to camera for fps, change to turret hole later
    public GameObject Emitter;

    void Start()

    {
        Emitter = this.gameObject;
    }

    void Shoot()
    {
        if (fireRate > 0)
        {
            // Reduce fire rate
            fireRate -= 1;
            Debug.Log(fireRate);
        }
        else
        {
            fireRate = 180;
            //creates a ray called hit
            RaycastHit hit;
            // this actually shoots out the ray. first variable is where the ray shoots from.  2nd is the direction (forward). 
            //3nd gathers info and puts it in hit, 4th is the range.
            if (Physics.Raycast(Emitter.transform.position, Emitter.transform.forward, out hit, PDDroneRange))
            {
                Debug.Log(hit.transform.name);
                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target != null)
                {
                    //This sends the damage variable of the drone
                    target.TakeDamage(PDDroneDamage);
                }

            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);

        // putting shoot on fire button just for testing
            Shoot();


    }










}

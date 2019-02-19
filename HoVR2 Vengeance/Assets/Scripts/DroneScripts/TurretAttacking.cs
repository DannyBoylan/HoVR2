using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttacking : MonoBehaviour
{
    // Use this for initialization

    public float PDDroneDamage = 10f;
    public float PDDroneRange = 500f;
    public int fireRate = 100;
    [Range(1f,10f)]
    public int rangeDivider = 1; // it divides the range which is a KM, chnage this to have what range you need for PD drones, between 1 and 10
    // this is so can attach shooting to camera for fps, change to turret hole later
    public GameObject Emitter;
    public GameObject Player;

    void Start()
    {
        Emitter = this.gameObject;
        Player = GameObject.Find("playerController");
    }


    void Shoot()
    {
        if (fireRate > 0)
        {
            // Reduce fire rate
           fireRate -= 1;
          // Debug.Log(fireRate);
        }
        else
        {
            fireRate = 180;
            //creates a ray called hit
            RaycastHit hit;
            // this actually shoots out the ray. first variable is where the ray shoots from.  2nd is the direction (forward). 3nd gathers info and puts it in hit, 4th is the range.
            if (Physics.Raycast(Emitter.transform.position, Emitter.transform.forward, out hit, PDDroneRange))
            {
               // Debug.Log(hit.transform.name);
                Enemy target = hit.transform.GetComponent<Enemy>();
                Debug.Log(Player.GetComponent<ProximityDetection>().rangeInKM / rangeDivider);

                if (target != null && Player.GetComponent<ProximityDetection>().rangeInKM / rangeDivider < PDDroneRange) // need to put something here that is less than range 
                {
                    //This sends the damage variable of the drone
                    target.TakeDamage(PDDroneDamage);

                    Debug.Log("TESTTTTTT");
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


    void OnDrawGizmosSelected()
    {
        // draws a wirefrane sphear to show the range of the spawner
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Player.GetComponent<ProximityDetection>().rangeInKM);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Player.GetComponent<ProximityDetection>().rangeInKM / rangeDivider);
    }







}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script uses some elements from the turret 
/// attacking script with some minor differences.
/// </summary>
public class ArDroneAttacking : MonoBehaviour {

    // Use this for initialization
    bool fireButton = false;
    public float ARDroneDamage = 100.0f;
    public float ARDroneRange = 10000f;

    [Range(0.0f, 5.0f)]
    public GameObject Emitter;

    public int Ammo = 5;
    public int MaxAmmo; // want max ammo to = ammo

    public float CoolDownTimer = 5.0f;
    public float MaxCoolDownTimer = 5.0f;
    public float ReloadTimer;  // want reload timer to be 2.5x cool down timer

    public GameObject impact;
    public GameObject thisObject;
    // this is the new dely system
    private bool allowfire = true;

    void Start ()
    {

        Emitter = this.gameObject;
        thisObject = this.gameObject;
    }


    IEnumerator Reload()
    {
        allowfire = false;

        

        // this waits for x seconds before the code runs again, uses corourines.
        yield return new WaitForSeconds(ReloadTimer);

        allowfire = true;

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireButton = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            fireButton = false;
        }

        //creates a ray called hit
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * ARDroneRange, Color.red);

        // this actually shoots out the ray. first variable is where the ray shoots from.  2nd is the direction (forward). 3nd gathers info and puts it in hit, 4th is the range.
        if (Physics.Raycast(Emitter.transform.position, Emitter.transform.forward, out hit, ARDroneRange))
        {
            //Debug.Log(hit.transform.name); // this displays the name of what is hit



            // putting shoot on fire button just for testing
            if (fireButton && allowfire)
            {
                //Debug.Log(hit.point);
                // this creates the impact at the end of the ray (the hit.point)

                GameObject spark = Instantiate(impact, hit.point, transform.rotation);                
                spark.GetComponent<TransformStorage>().gun = thisObject;



                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target != null)
                {
                    //This sends the damage variable of the drone
                    target.TakeDamage(ARDroneDamage);

                }
                // this calls the function SetEndPoint which sets the EndPoint of the lazer... maybe add weapon dely here could be more efficent coding

                StartCoroutine(Reload());


            }




        }


    }
}

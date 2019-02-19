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
    public int ReloadTimer;  // want reload timer to be 2.5x cool down timer

    void Start ()
    {

        Emitter = this.gameObject;
    }

    void Attack()
    {
        if (Ammo > 0 & CoolDownTimer == MaxCoolDownTimer)
        {
            Ammo -= 1;
            CoolDownTimer = 0.0f;

            //creates a ray called hit
            RaycastHit hit;
                // this actually shoots out the ray. first variable is where the ray shoots from.  2nd is the direction (forward). 3nd gathers info and puts it in hit, 4th is the range.
                if (Physics.Raycast(Emitter.transform.position, Emitter.transform.forward, out hit, ARDroneRange))
                {
                    Debug.Log(hit.transform.name); // this displays the name of what is hit

                    Enemy target = hit.transform.GetComponent<Enemy>();

                    if (target != null)
                    {
                        //This sends the damage variable of the drone
                        target.TakeDamage(ARDroneDamage);

                    }



                }

        }
        else if (Ammo <= 0)
        {
            CoolDownTimer = 0.0f;
            Ammo = 5;

        }
    }

    // Update is called once per frame
    void Update () {
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);

        // Increment cool down timer
        if (CoolDownTimer < MaxCoolDownTimer)
        {
            CoolDownTimer += 1 * Time.deltaTime;

        }
        else if(CoolDownTimer > MaxCoolDownTimer)
        {
            CoolDownTimer = MaxCoolDownTimer;


        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireButton = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            fireButton = false;
        }
        // putting shoot on fire button just for testing
        if (fireButton)
        {

            Attack();

        }
    }
}

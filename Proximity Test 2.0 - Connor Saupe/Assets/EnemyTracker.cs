using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public GameObject Player;
    public GameObject missileTarget;
    public Rigidbody missileRigidbody;
    public float turn;
    public float missileVelocity;
    public float MissileDamage = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (missileTarget != null)
            {
                missileTarget.GetComponent<Enemy>().TakeDamage(MissileDamage);
                Destroy(gameObject);
            }
            else
            {
                other.GetComponent<Enemy>().TakeDamage(MissileDamage);
                Destroy(gameObject);
                Debug.Log("HIT");
            }
        }
    }

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        missileTarget = Player.GetComponent<ProximityDetection>().closestEnemy;
        if (missileTarget != null)
        {
            missileRigidbody.velocity = transform.forward * missileVelocity;

            var missileTargetRotation = Quaternion.LookRotation(missileTarget.transform.position - transform.position);

            missileRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, missileTargetRotation, turn));
        }
        else
        {
            missileRigidbody.velocity = transform.forward * missileVelocity;
        }
    }

    private void Update()
    {
        
    }


}


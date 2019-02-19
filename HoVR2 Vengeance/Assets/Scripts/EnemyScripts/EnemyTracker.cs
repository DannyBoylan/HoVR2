using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public GameObject Player, missileTarget,Explosion;
    public Rigidbody missileRigidbody;
    public float turn;
    public float missileVelocity;
    public float MissileDamage = 50f;

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.tag == "enemy")
        {
            if (missileTarget != null)
            {
                missileTarget.GetComponent<Enemy>().TakeDamage(MissileDamage);
                Instantiate(Explosion, transform.position, transform.rotation);
                Destroy(gameObject);

            }
            else
            {
                other.GetComponent<Enemy>().TakeDamage(MissileDamage);
                Instantiate(Explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                Debug.Log("HIT");
            }
        }
    }

    private void Start()
    {
        Player = GameObject.Find("PlayerController");
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public GameObject Player, Explosion;
    public GameObject missileTarget;
    public Rigidbody missileRigidbody;
    public float turn;
    public float missileVelocity;
    public float MissileDamage = 50f;
    public bool destroy = false;
    IEnumerator Delaytime()
    {
        yield return new WaitForSeconds(5);
        destroy = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            Debug.Log("enemy");
            if (missileTarget != null)
            {
                missileTarget.GetComponent<Enemy>().TakeDamage(MissileDamage);
                Destroy(this.gameObject);
            }
            else
            {
                other.GetComponent<Enemy>().TakeDamage(MissileDamage);
                Destroy(this.gameObject);
                Debug.Log("HIT");
            }
        }
    }

    private void Start()
    {
        Player = GameObject.Find("playerController");
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
        StartCoroutine(Delaytime());

        if (destroy)
        {

            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
        }
    }


}


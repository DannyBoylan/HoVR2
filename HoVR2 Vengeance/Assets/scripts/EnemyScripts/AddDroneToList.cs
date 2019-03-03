using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDroneToList : MonoBehaviour
{
    public GameObject drone;
    private void Update()
    {
        GetComponent<SphereCollider>().radius = drone.GetComponent<EnemyTargeting>().range*10;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Turret")
        {
            drone.GetComponent<EnemyTargeting>().addToList(other.gameObject);
        }
    }
    
}

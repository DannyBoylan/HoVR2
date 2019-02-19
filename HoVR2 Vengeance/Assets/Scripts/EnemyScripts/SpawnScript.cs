using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public GameObject Player, WhatToSpawn;
    [Range(0.0f,10.0f)]
    public float range = 5;

    // Update is called once per frame
    void Update()
    {
        // gets distance from the player and if the player is in range, spawn an enemy and destroy the spawner
        if (Math.Distance(this.transform.position, Player.transform.position) < range*100){
            Instantiate(WhatToSpawn, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
    void OnDrawGizmosSelected()
    {
        // draws a wirefrane sphear to show the range of the spawner
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range*100);
    }
}

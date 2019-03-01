using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    //We need this to make sure the calculation in this script doesn't keep running when it's not needed.
    public bool added = false;
    public GameObject player;
    public float dist;
    public float range;
    // Update is called once per frame
    private void Start() {

        player = GameObject.Find("playerController");
    }
    void Update() {
        range = player.GetComponent<ProximityDetection>().range;
        //This is checking if it has been added to the list. if it has, it will not calculate the next part of the statement, this makes
        //having the bool first is more effieciant as checking if bool = 0 or 1 is cheaper computation-wise than getting the distance and checking
        //if it's lower than 10.0f
        dist = Math.Distance(player.transform.position, transform.position);
        if ((added == false)&&(Math.Distance(player.transform.position, transform.position) < range))
        {
            //This literally just calls the addToList Method in proximityDetection.cs and adds this as a gameObject, we set added to true, 
            //and proximityDetection.cs handles turning it off again
            player.GetComponent<ProximityDetection>().addToList(this.gameObject);
            added = true;
        }
    }
    //sets enemy health
    public float EnemyHealth = 50f;
  
    public void TakeDamage(float amount)
    {
        EnemyHealth -= amount;
        if (EnemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    
    }


}

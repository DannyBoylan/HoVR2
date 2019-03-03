using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    public List<GameObject> drones;
    public GameObject closestdrone;
    public float range = 1;
    float currentDistance;
    // Start is called before the first frame update
    void Start()
    {

        range = range * 10;
       
    }

    public void addToList(GameObject A)
    {
        bool beenAdded = false;
        for (int i = 0; i < drones.Count; i++)
        {
            
            if (drones[i] == A)
            {
                beenAdded = true;
            }
        }
        if (!beenAdded)
        {
            drones.Add(A);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        //We make sure this is a high number as we will be calculating it lower to get the lowest number(Closest Distance) 
        
        if (drones.Count == 0)
        {
            closestdrone = GameObject.Find("playerController");
        }

        else
        {
            float distance = 100000.0f;
            //This is the basic for loop that will run through every enemy that is currently in the enemies list
            for (int i = 0; i < drones.Count; i++)
            {
                //We need this if statement to check whether there is a NULL in the list. say we shoot an enemy and destroy the object. this will then remove it from the list
                //if there are no enemies close enough, or they are all destroyed, this will remove the closest enemy as there should be no enemies
                if (drones[i] == null)
                {
                    drones.Remove(drones[i]);
                }
                if(drones[i].GetComponent<DroneLife>().health <= 0)
                {
                    drones.Remove(drones[i]);
                }
                //This is the code to check distance and set the distance lower and change the closestEnemy to the closest enemy
                currentDistance = Math.Distance(this.transform.position, drones[i].transform.position);
                if (distance > currentDistance)
                {
                    distance = currentDistance;
                    closestdrone = drones[i];
                }
            }
        }

        if (closestdrone != null)
        {
            transform.LookAt(closestdrone.transform.position);
        }

    }


    void OnDrawGizmosSelected()
    {
        // draws a wirefrane sphear to show the range of the spawner
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range * 10);
    }
}

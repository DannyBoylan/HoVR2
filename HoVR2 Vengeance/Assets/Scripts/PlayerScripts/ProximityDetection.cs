using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDetection : MonoBehaviour
{

    //We need to add a list of enemies to know what is in range and then from that we can use the currentDistance to set the closestEnemy
    //to the enemy that is closest to the turret.
    public List<GameObject> enemies;
    public GameObject closestEnemy;
    public float rangeInKM = 1;
    float currentDistance;


    private void Start()
    {
        rangeInKM = rangeInKM * 500;
    }



    //This is a public method that can be called to add game objects to the list. this will be called in the Enemy.cs script.
    public void addToList(GameObject A)
    {
        enemies.Add(A);

    }

    // Update is called once per frame
    void Update()
    {


        //We make sure this is a high number as we will be calculating it lower to get the lowest number(Closest Distance) 
        float distance = 100000.0f;


        //This is the basic for loop that will run through every enemy that is currently in the enemies list
        for (int i = 0; i < enemies.Count; i++)
        {

            //We need this if statement to check whether there is a NULL in the list. say we shoot an enemy and destroy the object. this will then remove it from the list
            //if there are no enemies close enough, or they are all destroyed, this will remove the closest enemy as there should be no enemies
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }

            //if it is in the list, we need to make sure it's in range, if it is out of range, then we again remove it from the list. if it comes back
            //into range, the Enemy.cs script will add it back in for us.
            else if (Math.Distance(this.transform.position, enemies[i].transform.position) > rangeInKM)
            {
                enemies[i].GetComponent<Enemy>().added = false;
                enemies.Remove(enemies[i]);
            }
            if (enemies.Count == 0)
            {
                closestEnemy = null;
                break;
            }

            //This is the code to check distance and set the distance lower and change the closestEnemy to the closest enemy
            currentDistance = Math.Distance(this.transform.position, enemies[i].transform.position);
            if (distance > currentDistance)
            {
                distance = currentDistance;
                closestEnemy = enemies[i];
            }
        }


    }
    void OnDrawGizmosSelected()
    {
        // draws a wirefrane sphear to show the range of the spawner
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeInKM*500);
    }
}

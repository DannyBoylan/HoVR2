using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowScript : MonoBehaviour
{
    public GameObject player;
    public float SpeedMultiplier = 2;
    // Update is called once per frame
    private void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("playerController");
        }
    }
    void Update()
    {
        //checks the distance and follow the player within that distance
        if (Math.Distance(this.transform.position, player.transform.position) > 10)
        {

            //very basic could be improved, have it follow a vector with speed and acceleration that follows the player, will make movement smoother
            Vector3 direction = player.transform.position - transform.position;
            transform.position += (Math.NormVector(direction) * SpeedMultiplier);

        }
        //destroy if too far away
        transform.LookAt(player.transform.position);
        if (Math.Distance(this.transform.position, player.transform.position) > 1500)
        {
            Destroy(this.gameObject);
        }
    }
}

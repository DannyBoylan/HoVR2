using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// www.youtube.com/watch?v=m0fjrQkaES4 //

public class MakeCollider : MonoBehaviour {
    public List<GameObject> EnemyList;
    public bool debug = false; // Debug mode
    public bool collided = false; // Store if collision has occurred
    Renderer rend;

    public bool activateEnemies, setRadius;
    public int RadiusSize = 0;
    public void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collided = true;
        }
        
    }
    // Use this for initialization
    void Start () {
        rend = this.GetComponent<MeshRenderer>();
        if (!debug)
        {
            rend.enabled = false;
        }
	}

    // Update is called once per frame
    void Update ()
    {
        if (collided == true)
        {

            if (setRadius)
            {
                GameObject.Find("playerController").GetComponent<PlayerMovement>().SetRadius(RadiusSize);
            }

            if (activateEnemies)
            {
                if (EnemyList.Count > 0)
                {
                    for (int i = 0; i < EnemyList.Count; i++)
                    {
                        EnemyList[i].GetComponent<SplineWalkerEnemy>().active = true;
                    }
                }
            }

            Destroy(this.gameObject);
        }
	}
    void OnDrawGizmosSelected()
    {
        float gizRad = 0;
        switch(RadiusSize){
            case 0:
                gizRad = 10;
                break;
            case 1:
                gizRad = 45;
                break;
            case 2:
                gizRad = 90;
                break;
            case 3:
                gizRad = 180;
                break;
            case 4:
                gizRad = 360;
                break;
            default:
                gizRad = 0;
                break;
        }
        // draws a wirefrane sphear to show the range of the spawner
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, gizRad);
    }
}

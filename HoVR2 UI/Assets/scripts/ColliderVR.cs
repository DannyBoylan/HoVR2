using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// www.youtube.com/watch?v=m0fjrQkaES4 //

public class ColliderVR : MonoBehaviour {
    public List<GameObject> EnemyList;
    public bool debug = false; // Debug mode
    public bool collided = false; // Store if collision has occurred
    Renderer rend;
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
    void Update () {
        if (collided == true)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                EnemyList[i].GetComponent<SplineWalkerEnemy>().active = true;
            }
            Destroy(this.gameObject);
        }
	}
}

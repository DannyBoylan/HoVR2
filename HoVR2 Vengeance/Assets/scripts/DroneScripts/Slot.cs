using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject Drone;
    public bool isActive = false;
    [SerializeField]
    bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (!spawned)
            {
                GameObject drone = Instantiate(Drone, transform.position, transform.rotation,this.transform);
                drone.GetComponent<DroneLife>().slot = this.gameObject;
                spawned = true;
                
            }
        }
    }
    public void DroneDestroyed()
    {
        isActive = false;
        spawned = false;   
    }
}

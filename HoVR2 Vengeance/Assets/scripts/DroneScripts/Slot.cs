using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject Drone;
    public bool isActive = false;
    [SerializeField]
    bool spawned = false;
    // Update is called once per frame
    void Update()
    {
        //this will run if the slot controller sets it to active. if it is active, it will then check to see if
        //it has spawned a drone, if not, it will spawn a drone and set the drones to this slot.
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
        //resets the slot to accept a new drone if the drone on it has been destroyed.
        isActive = false;
        spawned = false;   
    }
}

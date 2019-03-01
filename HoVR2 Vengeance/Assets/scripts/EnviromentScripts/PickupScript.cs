using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public bool PdDrone = false;
    public bool ArDrone = false;
    public bool MissileDrone = false;
    bool collided = false;

    //Collider for the pickup object
    public void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collided = true;
        }
    }



    private void Update()
    {
        //this checks what the pickup shoould add and adds it to the correct slot controller to add the drone to a slot if one is available
        if (collided)
        {
            GameObject other = GameObject.Find("playerController");
            if (PdDrone)
            {
                other.GetComponentInParent<SlotSorter>().PDslots.GetComponent<slotController>().PickedUpDrone();
                Destroy(this.gameObject);
            }

            else if (ArDrone)
            {
                other.GetComponentInParent<SlotSorter>().ARslots.GetComponent<slotController>().PickedUpDrone();
                Destroy(this.gameObject);
            }

            else if (MissileDrone)
            {
                other.GetComponentInParent<SlotSorter>().MissileSlots.GetComponent<slotController>().PickedUpDrone();
                Destroy(this.gameObject);
            }
        }
    }
}
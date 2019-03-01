using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotController : MonoBehaviour
{
    public List<GameObject> slots;
    public bool debugPickup = false;

    // Update is called once per frame
    void Update()
    {
        //This is just Debug code, clicking it runs the pickup 
        //code one, such as if you were to collide with an object
       if (debugPickup)
        {
            PickedUpDrone();
            debugPickup = false;
            
        } 
    }

    //this is the method called to add a drone. it checks the slots to see if one is free
    //if one is, then it will activate that slot, which in that slots code will spawn
    //a drone on the slot
    public void PickedUpDrone()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetComponent<Slot>().isActive == false)
            {
                slots[i].GetComponent<Slot>().isActive = true;
                break;
            }
        }
    }
}

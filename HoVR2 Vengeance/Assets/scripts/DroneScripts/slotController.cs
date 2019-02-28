using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotController : MonoBehaviour
{
    public List<GameObject> slots;
    public bool debugPickup = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (debugPickup)
        {
            PickedUpDrone();
            debugPickup = false;
            
        } 
    }
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

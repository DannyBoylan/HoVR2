using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arduino : MonoBehaviour {

    
    public float speed;
    public void Update()
    {
        speed = GetComponent<PlayerMovement>().speed;

        if (speed > 0)
        {
            this.GetComponent<ArduinoComunicator>().setRumbleSpeed((int)speed);

            this.GetComponent<ArduinoComunicator>().setFan1Speed((int)speed);

            this.GetComponent<ArduinoComunicator>().setFan2Speed((int)speed);
        }
        else if (speed <= 0)
        {
            this.GetComponent<ArduinoComunicator>().setRumbleSpeed(0);

            this.GetComponent<ArduinoComunicator>().setFan1Speed(0);

            this.GetComponent<ArduinoComunicator>().setFan2Speed(0);
        }
    }
}

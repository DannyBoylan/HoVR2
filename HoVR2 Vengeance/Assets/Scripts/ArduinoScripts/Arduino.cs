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
            this.GetComponent<ArduinoComunicator>().setSpeed(speed);
        }
        else if (speed <= 0)
        {
            this.GetComponent<ArduinoComunicator>().setArduinoOff();
        }

    }
}

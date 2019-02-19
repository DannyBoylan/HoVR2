using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;
using UnityEngine;

public class ArduinoComunicator : MonoBehaviour
{

    public static SerialPort sp = new SerialPort("COM5", 9600);
    public static SerialPortLineReader reader = new SerialPortLineReader(sp);
    public string message2;

    void Start()
    {
        OpenConnection();
    }

    void Update()
    {
        // Debug to test whether the port has timed out.
        message2 = reader.ReadLine();
        print(message2);
    }

    public void OpenConnection()
    {
        if(sp != null)
        {
            if(sp.IsOpen)
            {
                sp.Close();
                print("Closing port, because it was already open!");
            }
            else
            {
               sp.Open();
               print("Port is open");
               sp.ReadTimeout = 16;
            }
        }
        else
        {
            if(sp.IsOpen)
            {
                print("Port is already open");
            }
            else
            {
                print("Port == null");
            }
        }
    }

   public void OnApplicationQuit()
    {
        print("Closed application");
        setArduinoOff();
        
        sp.Close();
    }

    public void setRumbleSpeed(int speed)
    {
       sp.Write(speed.ToString());
       Debug.Log(speed.ToString());
    }
	    public void setFan1Speed(int speed)
    {        
        sp.Write("b,"+speed);        
    }
    public void setFan2Speed(int speed)
    {       
        sp.Write("c,"+speed);        
    }
	public void setArduinoOff()
	{
		sp.Write("a,0");
		sp.Write("b,0");
		sp.Write("c,0");
	}

}
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
    char isoff = 'a';

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
               sp.ReadTimeout = 1000;
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

    public void setSpeed(float speed)
    {
        if      (speed > 90)    { if (isoff != 'j') { sp.Write("j"); isoff = 'j'; } }
        else if (speed > 80)    { if (isoff != 'i') { sp.Write("i"); isoff = 'i'; } }
        else if (speed > 70)    { if (isoff != 'h') { sp.Write("h"); isoff = 'h'; } }
        else if (speed > 60)    { if (isoff != 'g') { sp.Write("g"); isoff = 'g'; } }
        else if (speed > 50)    { if (isoff != 'f') { sp.Write("f"); isoff = 'f'; } }
        else if (speed > 40)    { if (isoff != 'e') { sp.Write("e"); isoff = 'e'; } }
        else if (speed > 30)    { if (isoff != 'd') { sp.Write("d"); isoff = 'd'; } }
        else if (speed > 20)    { if (isoff != 'c') { sp.Write("c"); isoff = 'c'; } }
        else if (speed > 5)     { if (isoff != 'b') { sp.Write("b"); isoff = 'b'; } }
        else if (speed <= 5) setArduinoOff();
    }
	public void setArduinoOff()
	{
        if (isoff != 'a')
        {
            sp.Write("a");
            isoff = 'a';
        }
	}

}
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;
using UnityEngine;

public class SerialPortLineReader
{
    /// <What_The_Serial_Port_Line_Reader_Does>
    ///  This is used to handle the hardware input/output.
    ///  
    ///  Seperating this out from the main communication loop ensures you onlu sync when
    ///  you want to access data.
    ///  
    /// First we declare access to the serial port from the arduino. And the threads we 
    /// are going to read from it.
    /// 
    /// We then declare access to an object which will allow us to ensure that no other thread
    /// can be active whilst another operation is going on. 
    ///  
    /// <End_Of_Summary>

    private SerialPort sp;
    private Thread readLoop;
    private object lockHandle = new object();
    private List<string> lines = new List<string>();

    // This allows us to check whether this is data being passed through the thread.
    public bool IsDataAvailable
    {
        get
        {
            lock (lockHandle)
            {
                return lines.Count > 0;
            }
        }
    }

    // Reads through the threads to check if an input has been received.
    // Ensures that the empty thread is removed and the new value is protected.
    public string ReadLine()
    {
        string newPort = "Port has been timed out. Available for access";
        lock (lockHandle)
        {
            if (lines.Count > 0)
            {
                newPort = lines[0];
                lines.RemoveAt(0);
            }
        }
        return newPort;
    }

    // Instantiates the threads for the line reader. Declares that the thread being used is a background thread and executes it.
    public SerialPortLineReader(SerialPort threadSerialPort)
    {
        sp = threadSerialPort;
        readLoop = new Thread(threadFunc)
        {
            IsBackground = true
        };
        readLoop.Start();
    }

    // Sets up the initial thread. This gets a value containing the state of the current thread.
    // If it's not being requested to stop, then add the port to the end of the list.
    private void threadFunc()
    {
        while (readLoop.ThreadState != ThreadState.StopRequested)
        {
            string newPort = sp.ReadLine();
            lock (lockHandle)
            {
                lines.Add(newPort);
            }
        }
    }
}

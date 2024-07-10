using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class EMGRawReader : MonoBehaviour
{
    public string IP = "127.0.0.1";
    private int port = 12346;
    private int sendPort = 12347;
    public string readVal = "";
    public string timestamp = "";
    public float velocity;
    public float[] prob_vector = new float[5];

    // read Thread
    Thread readThread;
    // udpclient object
    UdpClient client;
    UdpClient server;
    IPEndPoint serverTarget;
    
    private static EMGRawReader playerInstance;

    void Awake() 
    {
        DontDestroyOnLoad (this);
         
        if (playerInstance == null) {
            playerInstance = this;
        } else {
            DestroyObject(gameObject);
        }
        StartReadingData(); //Somethign weird happening
    }

    public void StartReadingData()
    {
        // create thread for reading UDP messages
        readThread = new Thread(new ThreadStart(ReceiveData));
        readThread.IsBackground = true;
        readThread.Start();
        server = new UdpClient(sendPort);
        serverTarget = new IPEndPoint(IPAddress.Parse(IP), sendPort);
    }

    // Unity Application Quit Function
    void OnApplicationQuit()
    {
        stopThread();
    }

    // Stop reading UDP messages
    public void stopThread()
    {
        if (readThread.IsAlive)
        {
            readThread.Abort();
        }
        server.Close();
        client.Close();
    }

    // receive thread function
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                // receive bytes
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] buff = client.Receive(ref anyIP);

                // encode UTF8-coded bytes to text format
                string text = Encoding.UTF8.GetString(buff);
                string[] splitText = text.Split();

                string prediction = "";
                
                readVal = splitText[0];

                    
                velocity = float.Parse(splitText[1], CultureInfo.InvariantCulture.NumberFormat);
                timestamp = splitText[6];
                
                
            }
            catch (Exception err)
            {
                Debug.Log(err.ToString());
                // print(err.ToString());
            }
        }
    }

    public void Write(string strMessage) 
    {
        byte[] arr = System.Text.Encoding.UTF8.GetBytes(strMessage);
        server.Send(arr, arr.Length, serverTarget);
    }
}

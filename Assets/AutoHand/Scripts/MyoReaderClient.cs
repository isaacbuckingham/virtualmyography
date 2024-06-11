using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using TMPro;

using System.Threading;

public class MyoReaderClient : MonoBehaviour {

    private int port = 8099;
    private string host = "192.168.137.168"; 
    private string portUWP = "8099";
    public string control = "Starting!";
    public int consecutive = 0;
    private StreamReader reader;
    private bool connected = false;

    TcpClient socketClient;

    void Start () {
        ConnectSocketUnity();
        var readThread = new Thread(new ThreadStart(ListenForDataUnity));
        readThread.IsBackground = true;
        readThread.Start();
    }
    
    void Update () {
        Debug.Log(control);
    }
    void ConnectSocketUnity()
    {
        IPAddress ipAddress = IPAddress.Parse(host);

        socketClient = new TcpClient();
        try
        {
            socketClient.Connect(ipAddress, port);
        }

        catch
        {
            Debug.Log("error when connecting to server socket");
        }
    }
    void ListenForDataUnity()
    {
        int data;
        while(true){
            byte[] bytes = new byte[socketClient.ReceiveBufferSize];
            NetworkStream stream = socketClient.GetStream();
            data = stream.Read(bytes, 0, socketClient.ReceiveBufferSize);
            string tControl = Encoding.UTF8.GetString(bytes, 0, data).Trim();
            // Keep track of consecutive agreements
            if (tControl == control) {
                consecutive += 1; 
            } else {
                consecutive = 0;
            }
            control = tControl;

        }
    }
}
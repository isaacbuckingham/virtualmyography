using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System;

//This Class if made for synchronous TCP communication in Unity
public class TcpClientManager : MonoBehaviour
{
    public string serverIP = "127.0.0.1";  // Server IP address
    public int serverPort = 12345;         // Server port number

    private TcpClient client;
    private NetworkStream stream;
    private byte[] buffer = new byte[1024];

    private void Start()
    {
        ConnectToServer();
    }

    private void Update()
    {
        if(stream.DataAvailable)
        {
            // Receive response from the server (optional)
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            //Process the received data
            ProcessReceivedData(receivedData);
        }
    }

    private void ProcessReceivedData(string data)
    {
        Debug.Log("Received response from server: " + data);
    }

    private void OnDestroy()
    {
        DisconnectFromServer();
    }

    private void ConnectToServer()
    {
        try
        {
            client = new TcpClient();
            client.Connect(serverIP, serverPort);
            stream = client.GetStream();
            Debug.Log("Connected to server.");
        }
        catch (Exception e)
        {
            Debug.Log("Failed to connect to the server: " + e.Message);
        }

    }

    private void DisconnectFromServer()
    {
        if (client != null)
        {
            stream.Close();
            client.Close();
            Debug.Log("Disconnected from server.");
        }
    }

    public void SendMessageToServer(string message)
    {
        try
        {
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
        stream.Flush();
        Debug.Log("Sent message to server: " + message);
        }
        catch (Exception e)
        {
            Debug.LogError("Error sending data: " + e.Message);
        }
    }
}

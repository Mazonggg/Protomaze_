using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// Handles all communication with the server.
/// </summary>
public class SocketObject {

	private static string serverError = "Error";

	private Thread socketThread;
	private Socket socket;

    private int test = 0;
    private bool active = true;

	private static int port = 8050;
	private static IPAddress IPv4 = IPAddress.Parse("81.169.245.94");

	private IPEndPoint endPoint = new IPEndPoint(IPv4, port);

	public SocketObject(){

		// Create the socket, that communicates with server.
		socket = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

    float timeStamp = 0, interval = 10;

	public void WorkOnSocket(){

        //while(active){

			// Automatisch in Intervallen Socket ansprechen.
			if (Time.realtimeSinceStartup >= timeStamp + interval)
			{

				timeStamp = Time.realtimeSinceStartup;
				Debug.Log ("WorkOnSocket");
				// Transmitted data
				byte[] sendbuf = System.Text.ASCIIEncoding.ASCII.GetBytes("Hallo Server:" + test++);
				socket.SendTo(sendbuf, endPoint);
			}
       // }
    }
}

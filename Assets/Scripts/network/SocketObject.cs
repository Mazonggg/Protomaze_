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

	private int maxTalk = 100;

	private Timer timer;

	public void WorkOnSocket(){
		
		new Thread(() => 
			{
				Thread.CurrentThread.IsBackground = true; 

				TellSocket("Hallo Server:" + test++, 0);
			}).Start();
        
    }

	private void TellSocket(string msg, int talks){

		if (talks < maxTalk && active) {
			Debug.Log ("WorkOnSocket");
			// Transmitted data
			byte[] sendbuf = System.Text.ASCIIEncoding.ASCII.GetBytes (msg);
			int sendBytes = socket.SendTo (sendbuf, endPoint);
			Debug.Log ("sendBytes = " + sendBytes);

			Thread.Sleep (10000);
			TalkToSocket (msg, talks++);
			return;
		}
		active = false;
		return;
	}
}

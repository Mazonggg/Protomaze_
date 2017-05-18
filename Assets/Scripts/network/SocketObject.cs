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
class SocketObject {

	private static string serverError = "Error";

	private Thread socketThread;
	private Socket socket;

	private static int port = 6666;
	private static IPAddress IPv4 = new IPAddress(new byte[]{81, 169, 245, 94});

	private IPEndPoint endPoint = new IPEndPoint(IPv4, port);

	public SocketObject(){

		// Create the socket, that communicates with server.
		socket = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		// Transmitted data
		byte[] sendbuf = Encoding.ASCII.GetBytes("userName=Protofox,pwd=" + Constants.NetworkRoutines.Md5Sum("Protofox"));
	}

	/*
	 * Runs the Thread.
	 * 
	 * // TODO necassarily public???
	 */
	public void WorkOnSocket(){

	}
}

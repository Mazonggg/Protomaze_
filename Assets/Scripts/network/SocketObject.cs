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

	private static int port = 6666;
	private static IPAddress IPv4 = new IPAddress(new byte[]{81, 169, 245, 94});

	private IPEndPoint endPoint = new IPEndPoint(IPv4, port);

	public SocketObject(){

		// Create the socket, that communicates with server.
		socket = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
       

    }

	/*
	 * Runs the Thread.
	 * 
	 * // TODO necassarily public???
	 */
	public string WorkOnSocket(){

        // Transmitted data
        byte[] sendbuf = Encoding.ASCII.GetBytes("Hallo Server:" + test++);
        socket.Send(sendbuf);
        return test.ToString();
    }
}

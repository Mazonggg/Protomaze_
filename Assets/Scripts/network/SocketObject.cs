﻿using System.Collections;
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
	public bool Active {
		get { return active; }
		set { active = value; }
	}

	private static int port = 8050;
	private static IPAddress IPv4 = IPAddress.Parse("81.169.245.94");

	private IPEndPoint endPoint = new IPEndPoint(IPv4, port);

	public SocketObject(){

		// Create the socket, that communicates with server.
		socket = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

	/// <summary>
	/// Starts to work on socket.
	/// Connection issues can be caught here.
	/// </summary>
	public void WorkOnSocket(){

		active = true;
		Constants.SoftwareModel.NetwRout.CheckSocket ();
		Constants.SoftwareModel.StartCoroutine (TellSocket());
		Constants.SoftwareModel.StartCoroutine (ListenToSocket());
    }

	// storage for upstream data.
	byte[] sendBuf = new byte[128];
	int sendBytes = 0;
	/// <summary>
	/// Tells change in state of CObjects to server.
	/// </summary>
	/// <returns>The socket.</returns>
	private IEnumerator TellSocket(){

		yield return new WaitForSeconds(1f);

		SendDatagram();
		while (active) {
			// Transmitted data
			if (Constants.SoftwareModel.UserHandler.ThisUser.Updated) {
				SendDatagram ();
			} 
			yield return new WaitForSeconds(0.01f);
		}
		//Debug.Log ("Ende TellSocket()");
	}

	private int counting = 0;
	private void SendDatagram() {

		//sendBuf = System.Text.ASCIIEncoding.ASCII.GetBytes ("HALLO " + counting++);
		sendBuf = System.Text.ASCIIEncoding.ASCII.GetBytes (CollectUserData ());
		sendBytes = socket.SendTo (sendBuf, endPoint);
	}


	// storage for upstream data.
	byte[] receiveBuf = new byte[128];
	/// <summary>
	/// Listens to downstream socket, connected to server, and updates state of CObjects accordingly.
	/// </summary>
	/// <returns>The to socket.</returns>
	private IEnumerator ListenToSocket (){

		yield return new WaitForSeconds(1f);

		while (active) {
			yield return null;
			if (socket.Poll(0, SelectMode.SelectRead)) {
				int bytesReceived = socket.Receive(receiveBuf, 0, receiveBuf.Length, SocketFlags.None);

				if (bytesReceived > 0) {
					ProcessDownBuf (receiveBuf);
				}
			}
		}		
		Debug.Log ("ListenToSocket: " + loggg[0]);
		Debug.Log ("ListenToSocket: " + loggg[1]);
		Debug.Log ("ListenToSocket: " + loggg[2]);
		Debug.Log ("ListenToSocket: " + loggg[3]);

		loggg [0] = "";
		loggg [1] = "";
		loggg [2] = "";
		loggg [3] = "";
	}

	private string[] loggg = {"", "", "", ""};

	/// <summary>
	/// Processes the content of the buf, received from server.
	/// checks, if content is valid, and sorts the information.
	/// </summary>
	/// <param name="buf">Buffer.</param>
	private void ProcessDownBuf(byte[] buf) {
		
		string bufString = System.Text.ASCIIEncoding.ASCII.GetString (buf);

		if (loggg[0].Equals("")) {
			loggg [0] = bufString;
			loggg [1] = "OWN TIME: " + DateTime.Now.ToString ("HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
		} else {
			loggg [2] = bufString;
			loggg [3] = "OWN TIME: " + DateTime.Now.ToString ("HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
		}
	}

	int counter = 0;
	/// <summary>
	/// Collects the data relevant for server update of this player and converts it to string convention.
	/// </summary>
	/// <returns>The user data.</returns>
	private string CollectUserData() {

		if (Constants.SoftwareModel.UserHandler.ThisUser.Updated) {
			UpdateData userData = Constants.SoftwareModel.UserHandler.ThisUser.UpdateData;

			string msg = "t=";
			if (userData.ObjectHeld == null) {
				msg += "1";
			} else {
				msg += "2";
			}

			msg += "&ui=" + userData.Id;
			msg += "&up=" +
			userData.Position.x + "_" +
			userData.Position.y + "_" +
				userData.Position.z + ";" +
			userData.Rotation.x + "_" +
			userData.Rotation.y + "_" +
			userData.Rotation.z;

			if (userData.ObjectHeld != null) {
				msg += "&oi=" + userData.ObjectHeld.Id;
				msg += "&op=" +
					userData.ObjectHeld.Position.x + "_" +
					userData.ObjectHeld.Position.y + "_" +
					userData.ObjectHeld.Position.z + ";" +
					userData.ObjectHeld.Rotation.x + "_" +
					userData.ObjectHeld.Rotation.y + "_" +
					userData.ObjectHeld.Rotation.z;
			}

			// Debug.Log("CollectUserData: " + msg);
			return msg;
		}
		return "";
	}
}

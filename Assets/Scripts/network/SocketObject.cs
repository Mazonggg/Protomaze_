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

	public void WorkOnSocket(){

		Constants.SoftwareModel.NetwRout.CheckSocket ();
		Constants.SoftwareModel.StartCoroutine (TellSocket());
    }

	private IEnumerator TellSocket(){

		while (active) {
			// Debug.Log ("WorkOnSocket");
			// Transmitted data
			if (Constants.SoftwareModel.UserHandler.ThisUser.Updated) {
				byte[] sendbuf = System.Text.ASCIIEncoding.ASCII.GetBytes (CollectUserData ());
				int sendBytes = socket.SendTo (sendbuf, endPoint);
				// Debug.Log ("sendBytes = " + sendBytes);
			} else {
				// Debug.Log ("nothing changed");
			}
			// Limit number of calls during testing.
			yield return new WaitForSeconds(0.2f);
			TellSocket ();
		}

		active = false;
		yield return new WaitForSeconds(2.0f);
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

			Debug.Log("CollectUserData: " + msg);
			return msg;
		}
		return "";
	}
}

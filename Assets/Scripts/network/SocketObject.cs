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
	private Socket socketUp;
	private Socket socketDown;


    private int test = 0;
    private bool active = true;
	public bool Active {
		get { return active; }
		set { active = value; }
	}

	private static int portUp = 8050;
	private static int portDown = 8051;
	private static IPAddress IPv4 = IPAddress.Parse("81.169.245.94");

	private IPEndPoint endPointUp = new IPEndPoint(IPv4, portUp);
	private IPEndPoint endPointDown = new IPEndPoint(IPAddress.Any, portDown);

	public SocketObject(){

		// Create the socket, that communicates with server.
		socketUp = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		socketDown = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

	/// <summary>
	/// Starts to work on socket.
	/// Connection issues can be caught here.
	/// </summary>
	public void WorkOnSocket(){

		active = true;
		Constants.SoftwareModel.NetwRout.CheckSocket ();
		Constants.SoftwareModel.StartCoroutine (TellSocket());

		PrepareSocketDown ();
		Debug.Log ("bis hier kommt er");
		// TODO Constants.SoftwareModel.StartCoroutine (ListenToSocket());
		Debug.Log ("und geht hier weiter");
    }

	// storage for upstream data.
	byte[] sendBufUp = new byte[]{0};
	int sendBytesUp = 0;
	/// <summary>
	/// Tells change in state of CObjects to server.
	/// </summary>
	/// <returns>The socket.</returns>
	private IEnumerator TellSocket(){

		while (active) {
			// Transmitted data
			if (Constants.SoftwareModel.UserHandler.ThisUser.Updated) {
				sendBufUp = System.Text.ASCIIEncoding.ASCII.GetBytes (CollectUserData ());
				sendBytesUp = socketUp.SendTo (sendBufUp, endPointUp);
			} 
			yield return new WaitForSeconds(0.1f);
		}
	}

	/// <summary>
	/// Prepares the downstream socket.
	/// </summary>
	/// <returns><c>true</c>, if socket down was prepared, <c>false</c> otherwise.</returns>
	private void PrepareSocketDown() {

		socketDown.Bind (endPointDown);
	}

	// storage for upstream data.
	byte[] sendBufDown = new byte[]{0};
	/// <summary>
	/// Listens to downstream socket, connected to server, and updates state of CObjects accordingly.
	/// </summary>
	/// <returns>The to socket.</returns>
	private IEnumerator ListenToSocket (){

		Debug.Log ("ListenToSocket()");
		while (active) {
			if (socketDown.Receive (sendBufDown) > 0) {         //CS: Change socketUp to socketDown
				ProcessDownBuf (sendBufDown);
				yield return new WaitForSeconds(0.1f);
			}
		}
	}

	/// <summary>
	/// Processes the content of the buf, received from server.
	/// checks, if content is valid, and sorts the information.
	/// </summary>
	/// <param name="buf">Buffer.</param>
	private void ProcessDownBuf(byte[] buf) {

		string bufString = System.Text.ASCIIEncoding.ASCII.GetString (buf);
		Debug.Log ("ProcessDownBuf: " + bufString);
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

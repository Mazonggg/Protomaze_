using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

/// <summary>
/// Represents the interface between the unity-game and the functional model of the Software in itself.
/// Establishes Connection to server.
/// </summary>
public class SoftwareModel : MonoBehaviour {

	private UserHandler userHandler;
	public UserHandler UserHandler {
		get { return userHandler; }
		set { userHandler = value; }
	}
	private SocketObject socketObj;
	public SocketObject SocketObj {
		get { return socketObj; }
		set { socketObj = value; }
	}
	private NetworkRoutines netwRout;
	public NetworkRoutines NetwRout {
		get { return netwRout; }
		set { netwRout = value; }
	}

	private int i = 0;
    // Use this for initialization
    void Start () {
		
		//gameObject.AddComponent<User> ();
		//userHandler = new UserHandler (GameObject.Find("User").GetComponent<User>());
		try {
			userHandler = GameObject.Find ("UserController").GetComponent<UserHandler> ();
			Debug.Log("userHandler found");
		} catch (Exception x) {}
		//UserHandler.AddUser (GameObject.Find ("User").GetComponent<User> ());
		//socketObj = new SocketObject ();
		netwRout = gameObject.GetComponent<NetworkRoutines> ();
	}

	public void PlaceUser() {

		if (userHandler != null) {

			userHandler.ThisUser.Place (new Vector3 (0, 0, 0), new Vector3(0, 0, 0), true);
		}
	}

	public void CreateSocketObject(){
		
		socketObj = new SocketObject ();
	}

	public void DestroySocketObject() {

		socketObj = null;
	}
}
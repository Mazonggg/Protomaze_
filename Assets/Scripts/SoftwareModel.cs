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

	public UserController userController;
	public NetworkRoutines netwRout;

	private SocketObject socketObj;

	public SocketObject SocketObj {
		get { return socketObj; }
		set { socketObj = value; }
	}

	private int i = 0;
    // Use this for initialization
    void Start () {
		
		//gameObject.AddComponent<User> ();
		//userHandler = new UserHandler (GameObject.Find("User").GetComponent<User>());
		/*try {
			userHandler = GameObject.Find ("UserController").GetComponent<UserHandler> ();
			//Debug.Log("userHandler found");
		} catch (Exception x) {}
		//UserHandler.AddUser (GameObject.Find ("User").GetComponent<User> ());
		//socketObj = new SocketObject ();
		netwRout = gameObject.GetComponent<NetworkRoutines> ();*/
	}

	public void PlaceUser() {

		if (userController != null) {

			userController.ThisUser.Place (new Vector3 (0, 0, 0), new Vector3(0, 0, 0), true);
		}
	}

	public void CreateSocketObject(int timer){
		
		socketObj = new SocketObject (timer);
		TimerScript.Timer = timer;
	}

	public void DestroySocketObject() {

		socketObj = null;
	}
}
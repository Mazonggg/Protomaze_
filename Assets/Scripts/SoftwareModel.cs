using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Represents the interface between the unity-game and the functional model of the Software in itself.
/// Establishes Connection to server.
/// </summary>
public class SoftwareModel : MonoBehaviour {

	private UserHandler userHandler;
	public UserHandler UserHandler {
		get { return userHandler; }
	}
    private SocketObject socketObj;
    public SocketObject SocketObj {
        get { return socketObj; }
        set { socketObj = value; }
    }

    // Use this for initialization
    void Start () {
		
		gameObject.AddComponent<User> ();
		userHandler = new UserHandler (gameObject.GetComponent<User>());
		socketObj = new SocketObject ();
		Constants.UserHandler = userHandler;
		Constants.NetworkRoutines = gameObject.AddComponent<NetworkRoutines> ();
		Constants.SocketObject = socketObj;
	}
}
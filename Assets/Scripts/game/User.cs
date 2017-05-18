using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serves as player controller.
/// </summary>
public class User : GObject {

	private bool isHost = true;
	public bool IsHost {
		get { return isHost; }
		set { isHost = value; }
	}

	void Start(){

	}

	void FixedUpdate(){
		
		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			Vector3 dir = new Vector3 (Input.GetAxis ("Horizontal"), 0,Input.GetAxis ("Vertical"));
			Move (dir, Constants.moveSpeed);
		}
	}
}

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
	private GObject objectHeld;
	public GObject ObjectHeld {
		get { return objectHeld; }
		set { objectHeld = value; }
	}
	/// <summary>
	/// Returns the relevant data for updating the server, for this object.
	/// </summary>
	/// <value>The update data.</value>
	public UpdateData UpdateData {
		get { 
			Updated = false;
			if (objectHeld == null) {
				return new UpdateData (
					Id, 
					new Vector3 (transform.position.x, transform.position.y, transform.position.z), 
					new Vector3 (transform.rotation.x, transform.rotation.y, transform.rotation.z)); 
			} else {
				return new UpdateData (
					Id, 
					new Vector3 (transform.position.x, transform.position.y, transform.position.z), 
					new Vector3 (transform.rotation.x, transform.rotation.y, transform.rotation.z),
					objectHeld.UpdateData); 
			}
		}
	}

	void FixedUpdate(){
		
		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			Vector3 dir = new Vector3 (Input.GetAxis ("Horizontal"), 0,Input.GetAxis ("Vertical"));
			Move (dir, Constants.moveSpeed);
		}
	}
}

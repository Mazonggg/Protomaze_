using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base-object for every movable object in a level.
/// </summary>
public class GObject : MonoBehaviour {

	private BoxCollider boxCollider;
	/// <summary>
	/// Database Id of this object on server.
	/// </summary>
	private int id = -1;
	public int Id
	{
		get { return id; }
		set { id = value; }
	}
	/// <summary>
	/// Real name of the object.
	/// </summary>
	private string objectName = "default";
	public string ObjectName{
		get { return objectName; }
        set { objectName = value; }
	}
	/// <summary>
	/// Tells, if the object currently moves or is moved by a user.
	/// </summary>
	private bool active = false;
	public bool Active {
		get { return active; }
		set { active = value; }
	}
	/// <summary>
	/// Database Id of session on server.
	/// </summary>
    private int ssId = -1;
    public int SsId {
        get { return ssId; }
        set { ssId = value; }
    }
	/// <summary>
	/// true, if changed since last time data where referenced.
	/// </summary>
	private bool updated = true;
	public bool Updated {
		get { return updated; }
		set { updated = value; }
	}
	/// <summary>
	/// Returns the relevant data for updating the server, for this object.
	/// </summary>
	/// <value>The update data.</value>
	public UpdateData UpdateData {
		get { 
			updated = false;
			return new UpdateData (
				id, 
				new Vector3(transform.position.x,transform.position.y,transform.position.z), 
				new Vector3(transform.rotation.x,transform.rotation.y,transform.rotation.z)); 
		}
	}

	/// <summary>
	/// Resets all parameter of this GObject.
	/// </summary>
	public void Restart() {
		transform.position = new Vector3 (0, 0, 0);
		//transform.rotation = new Vector3 (0, 0, 0);
		updated = true;
	}

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider>();
	}

	/*
	 * Moves GObject according to given direction and pace parameters.
	 */
	protected void Move(Vector3 dir, float pace){
		transform.position += dir * pace * Time.deltaTime;
		updated = true;
	}
}

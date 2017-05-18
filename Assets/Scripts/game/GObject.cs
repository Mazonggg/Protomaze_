using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GObject : MonoBehaviour {

	private BoxCollider boxCollider;
	private int id = -1;
	public int Id
	{
		get { return id; }
		set { id = value; Debug.Log("Id=" + value); }
	}
	private string objectName = "default";
	public string ObjectName{
		get { return objectName; }
        set { objectName = value; Debug.Log("ObjectName=" + value); }
	}
    private int ssId = -1;
    public int SsId {
        get { return ssId; }
        set { ssId = value; Debug.Log("ssId=" + ssId); }
    }
	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider>();
	}

	// Automatically called in constant time intervalls.
	void FixedUpdate() {
		
	}

	/*
	 * Moves GObject according to given direction and pace parameters.
	 */
	protected void Move(Vector3 dir, float pace){
		
		transform.position += dir * pace * Time.deltaTime;
	}
}

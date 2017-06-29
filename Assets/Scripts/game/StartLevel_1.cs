using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel_1 : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject.Find (Constants.softwareModel).GetComponent<SoftwareModel> ().CreateSocketObject ();
		GameObject.Find (Constants.softwareModel).GetComponent<SoftwareModel> ().PlaceUser();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

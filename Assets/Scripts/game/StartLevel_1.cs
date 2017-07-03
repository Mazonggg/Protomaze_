using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel_1 : MonoBehaviour {

	private static Vector3[] startPositions = {
		new Vector3 (0, 0, 0),
		new Vector3 (5, 0, 0),
		new Vector3 (-5, 0, 0),
		new Vector3 (0, -5, 0)
	};
	/// <summary>
	/// Gets the start position for a specific index of player in UserHandler.
	/// </summary>
	/// <returns>The start position.</returns>
	/// <param name="index">Index.</param>
	public static Vector3 GetStartPosition (int index) {

		return startPositions[index];
	}
	// Use this for initialization
	void Start () {
		
		GameObject.Find (Constants.softwareModel).GetComponent<SoftwareModel> ().CreateSocketObject ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

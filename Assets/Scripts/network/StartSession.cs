using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSession : MonoBehaviour {

	public GameObject /*createSessionCanvas,*/ startSessionButton;

    void Start () {
        //createSessionCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	// TODO implement logic that asks all other players to start their game-session.
	/// <summary>
	/// Sends the TCP Request to change Session status to "STARTING",
	/// Creates the SocketObject.
	/// </summary>
	public void StartTheSession(){

		SceneManager.LoadScene ("Level_1");
	}
}

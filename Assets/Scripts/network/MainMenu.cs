using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class MainMenu : MonoBehaviour {

	public GameObject logInCanvas, mainMenuCanvas;


	void Start(){
		mainMenuCanvas.SetActive (false);
	}
		
	public void LogoutUser() {
		
		string userId = Constants.UserHandler.ThisUser.Id.ToString();

		Constants.NetworkRoutines.TCPRequest(
			null, 
			"logoutUser",
			new string[] {"userId"},
			new string[] {userId});
	}

	public void StartSession() {

		string userId = Constants.UserHandler.ThisUser.Id.ToString();

		Constants.NetworkRoutines.TCPRequest(
			null, 
			"startSession",
			new string[] {"userId"},
			new string[] {userId});
	}

	private void RequestLogout(string userId) {
		

		mainMenuCanvas.SetActive (false);
		logInCanvas.SetActive (true);
	}

	private void RequestSessionStart(string userId) {

		mainMenuCanvas.SetActive (false);
		logInCanvas.SetActive (true);
	}
}

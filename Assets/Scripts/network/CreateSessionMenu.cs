using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class MainMenu : MonoBehaviour {

	public GameObject logInCanvas, mainMenuCanvas, startSessionButton, joinSessionCanvas, createSessionCanvas;
    public GameObject joinSessionController;

	void Start(){
		mainMenuCanvas.SetActive (false);
	}
		
	public void LogoutUser() {
		
		string userId = Constants.SoftwareModel.UserHandler.ThisUser.Id.ToString();
        //Debug.Log("LogoutUser: " + userId);

		Constants.SoftwareModel.NetwRout.TCPRequest(
            HandleLogout, 
			new string[] {"req", "userId"},
			new string[] {"logoutUser", userId});
	}

	private void HandleLogout(string[][] response) {

        logInCanvas.SetActive(true);
		mainMenuCanvas.SetActive(false);
        startSessionButton.SetActive(true);

		Constants.SoftwareModel.UserHandler.ThisUser.Id = -1;
		Constants.SoftwareModel.UserHandler.ThisUser.ObjectName = "default";
		Constants.SoftwareModel.SocketObj.Active = false;
	}

	/// <summary>
	/// Sets up request to server, that initiates the starting process of the session.
	/// </summary>
	public void StartSession() {

		Constants.SoftwareModel.UserHandler.ThisUser.Restart ();
		int IdTmp = Constants.SoftwareModel.UserHandler.ThisUser.SsId;
        //Debug.Log("IdTmp in StartSession: " + IdTmp);
        string sessionId = IdTmp.ToString();

		Constants.SoftwareModel.NetwRout.TCPRequest(
            InitiateSessionStart, 
			new string[] {"req", "sessionId"},
			new string[] {"startSession", sessionId});
	}

	/// <summary>
	/// Initiates the session start.
	/// </summary>
	/// <param name="response">Response.</param>
	private void InitiateSessionStart(string[][] response) {

		//Hide Menus, show loading screen
		Debug.Log("InitiateSessionStart()");
	}

	/*private void RequestLogout(string userId) {           // gebrauchen wir nicht

		mainMenuCanvas.SetActive (false);
		logInCanvas.SetActive (true);
	}*/                                                 


    public void JoinSession() {
        mainMenuCanvas.SetActive(false);
        joinSessionCanvas.SetActive(true);
        joinSessionController.GetComponent<JoinSession>().getSessions();        //Das die referenzierung bei MonoBehaviour so doof ist ...
        //  JoinSession js = new global::JoinSession();
        //  js.getSessions();
    }

	/// <summary>
	/// Sets up request to server, that creates the session
	/// </summary>
    public void CreateSession() {
		
		int IdTmp = Constants.SoftwareModel.UserHandler.ThisUser.Id;
		//Debug.Log("IdTmp in StartSession: " + IdTmp);
		string userId = IdTmp.ToString();

		Constants.SoftwareModel.NetwRout.TCPRequest(
			AssignSessionToUser, 
			new string[] {"req", "userId"},
			new string[] {"createSession", userId});
    }

	/// <summary>
	/// Assigns the session just created (on server) to the user on this client.
	/// </summary>
	/// <param name="response">Response.</param>
	private void AssignSessionToUser(string[][] response) {

		int SsIdTmp = -1;

		foreach (string[] pair in response) {

			if (pair [0].Equals ("type") && pair [1].Equals ("ERROR")) {

				Debug.Log ("Couldn't create Session!");
				return;
			}
			if(pair[0].Equals("sessionId")) {
				int.TryParse(pair[1], out SsIdTmp);
				Constants.SoftwareModel.UserHandler.ThisUser.SsId = SsIdTmp;
				Constants.SoftwareModel.SocketObj.WorkOnSocket();
				Debug.Log ("ssId=" + Constants.SoftwareModel.UserHandler.ThisUser.SsId + " tmp=" + SsIdTmp);

				mainMenuCanvas.SetActive(false);
				createSessionCanvas.SetActive(true);
				return;
			}
		}

	}
}

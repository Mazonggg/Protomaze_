using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class MainMenu : MonoBehaviour {

	public GameObject logInCanvas, mainMenuCanvas, createSessionCanvas, joinSessionCanvas;
    public GameObject joinSessionController;

	void Start(){
		mainMenuCanvas.SetActive (false);
	}
		
	public void LogoutUser() {

		string userId = Constants.GetUserId (0).ToString();
		//string userId = GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.Id.ToString();
        //Debug.Log("LogoutUser: " + userId);

		GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().NetwRout.TCPRequest(
            HandleLogout, 
			new string[] {"req", "userId"},
			new string[] {"logoutUser", userId});
	}

	private void HandleLogout(string[][] response) {

        logInCanvas.SetActive(true);
		mainMenuCanvas.SetActive(false);

		Constants.SetUserInfo(0, -1, "", "");
		//GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.Id = -1;
		//GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.ObjectName = "default";
		//GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().SocketObj.Active = false;
	}


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

		int IdTmp = Constants.GetUserId (0);
		//int IdTmp = GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.Id;
		//Debug.Log("IdTmp in StartSession: " + IdTmp);
		string userId = IdTmp.ToString();

		GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().NetwRout.TCPRequest(
			createSessionCanvas.GetComponentInChildren<CreateSession>().AssignSessionToUser, 
			new string[] {"req", "userId"},
			new string[] {"createSession", userId});
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class MainMenu : MonoBehaviour {

	public GameObject logInCanvas, mainMenuCanvas, startSessionButton;

	void Start(){
		mainMenuCanvas.SetActive (false);
	}
		
	public void LogoutUser() {
		
		string userId = Constants.UserHandler.ThisUser.Id.ToString();
        Debug.Log("LogoutUser: " + userId);

		Constants.NetworkRoutines.TCPRequest(
            HandleLogout, 
			new string[] {"req", "userId"},
			new string[] {"logoutUser", userId});
	}

	private void HandleLogout(string[][] response) {

        logInCanvas.SetActive(true);
		mainMenuCanvas.SetActive(false);
        startSessionButton.SetActive(true);

        Constants.UserHandler.ThisUser.Id = -1;
		Constants.UserHandler.ThisUser.ObjectName = "default";
	}

	public void StartSession() {

        int IdTmp = Constants.UserHandler.ThisUser.Id;
        Debug.Log("IdTmp in StartSession: " + IdTmp);
        string userId = IdTmp.ToString();

		Constants.NetworkRoutines.TCPRequest(
            AssignSessionToUser, 
			new string[] {"req", "userId"},
			new string[] {"startSession", userId});
	}

    private void AssignSessionToUser(string[][] response) {

        int SsIdTmp = -1;

        foreach (string[] pair in response) {
            if(pair[0].Equals("sessionId")) {
                int.TryParse(pair[1], out SsIdTmp);
            }
        }
        Constants.UserHandler.ThisUser.SsId = SsIdTmp;
        startSessionButton.SetActive(false);
		Constants.SocketObject.WorkOnSocket();
    }

	private void RequestLogout(string userId) {
		

		mainMenuCanvas.SetActive (false);
		logInCanvas.SetActive (true);
	}
}

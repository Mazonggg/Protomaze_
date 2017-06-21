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

	public void StartSession() {

		Constants.SoftwareModel.UserHandler.ThisUser.Restart ();
		int IdTmp = Constants.SoftwareModel.UserHandler.ThisUser.Id;
        //Debug.Log("IdTmp in StartSession: " + IdTmp);
        string userId = IdTmp.ToString();

		Constants.SoftwareModel.NetwRout.TCPRequest(
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
		Constants.SoftwareModel.UserHandler.ThisUser.SsId = SsIdTmp;
        startSessionButton.SetActive(false);

		Constants.SoftwareModel.SocketObj.WorkOnSocket();
    }

	/*private void RequestLogout(string userId) {           // gebrauchen wir nicht

		mainMenuCanvas.SetActive (false);
		logInCanvas.SetActive (true);
	}*/                                                 

    public void JoinSession()
    {
        mainMenuCanvas.SetActive(false);
        joinSessionCanvas.SetActive(true);
        joinSessionController.GetComponent<JoinSession>().getSessions();        //Das die referenzierung bei MonoBehaviour so doof ist ...
        //  JoinSession js = new global::JoinSession();
        //  js.getSessions();
    }

    public void CreateSession()
    {
        mainMenuCanvas.SetActive(false);
        createSessionCanvas.SetActive(true);
    }
}

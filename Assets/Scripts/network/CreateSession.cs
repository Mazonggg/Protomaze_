using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSession : MonoBehaviour {

	public GameObject logInCanvas, mainMenuCanvas, createSessionCanvas, startSessionButton, backButton;

    void Start () {
        createSessionCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void goBack() {
        mainMenuCanvas.SetActive(true);
        createSessionCanvas.SetActive(false);
    }


	/// <summary>
	/// Assigns the session just created (on server) to the user on this client.
	/// </summary>
	/// <param name="response">Response.</param>
	public void AssignSessionToUser(string[][] response) {

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
				//Debug.Log ("ssId=" + Constants.SoftwareModel.UserHandler.ThisUser.SsId + " tmp=" + SsIdTmp);

				mainMenuCanvas.SetActive(false);
				createSessionCanvas.SetActive(true);
				return;
			}
		}
	}
}

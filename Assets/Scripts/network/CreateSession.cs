using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class CreateSession : MonoBehaviour {

	public GameObject logInCanvas, mainMenuCanvas, createSessionCanvas, startSessionButton, backButton;
    public Text user_a, user_b, user_c, user_d, headline;
    private Text[] users;

    void Start () {
        createSessionCanvas.SetActive(false);
        users = new Text[] { user_a, user_b, user_c, user_d };
        
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
				GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.SsId = SsIdTmp;
				// Constants.SoftwareModel.SocketObj.WorkOnSocket();
				// Debug.Log ("ssId=" + Constants.SoftwareModel.UserHandler.ThisUser.SsId + " tmp=" + SsIdTmp);

				mainMenuCanvas.SetActive(false);
				createSessionCanvas.SetActive(true);
                StartUpdateLobby();
                return;
			}
		}
	}

    private IEnumerator UpdateLobby(){

        while (true) {

            string userSession = GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.SsId.ToString();
            GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().NetwRout.TCPRequest(
                UpdateView,
                new string[] { "req", "sessionID" },
                new string[] { "getPlayerInSession", userSession });


            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateView(string[][] response){
        headline.text ="Wait for Players in Session " + GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.SsId.ToString();
        string ret = "";
        foreach (string[] pair in response){

            if (pair[0].Equals("playerInSession")){

                ret += pair[1];
            }
            string pattern = @"//";
            string[] usernames = Regex.Split(ret, pattern);

            for(int i=0; i< usernames.Length; i++) {

                users[i].text = usernames[i];
            }

        }
    }

    public void StartUpdateLobby() {
        StartCoroutine(UpdateLobby());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinSessionButtonPrefab : MonoBehaviour {

    public Button joinSessionButton;
    public Text sessionIDText;
    public Text leaderText;
    public GameObject joinSessionCanvas, createSessionCanvas;


	// Use this for initialization
	void Start () {
        joinSessionButton.onClick.AddListener(MakeRequest);
    }

    public void SetUp (string sessionID, string leader) {
        sessionIDText.text = sessionID;
        leaderText.text = leader;
    }


    public void MakeRequest() {
        string userId = GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.Id.ToString();
        GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().NetwRout.TCPRequest(
            SetSessionIdAndGoToLobby,
            new string[] { "req", "sessId", "userId" },
            new string[] { "joinSession", sessionIDText.text, userId });
    }


    public void SetSessionIdAndGoToLobby(string[][] response)
    {

        int SsIdTmp = -1;

        foreach (string[] pair in response)
        {

            if (pair[0].Equals("type") && pair[1].Equals("ERROR"))
            {

                Debug.Log("User already assigned to a session or Session does not exist!");
                return;
            }
            if (pair[0].Equals("sessionId"))
            {
                int.TryParse(pair[1], out SsIdTmp);
                GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.SsId = SsIdTmp;

                joinSessionCanvas.SetActive(false);
                createSessionCanvas.SetActive(true);
                createSessionCanvas.GetComponentInChildren<CreateSession>().StartUpdateLobby();
                return;
            }
        }
    }

}

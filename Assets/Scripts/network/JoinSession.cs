﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class JoinSession : MonoBehaviour {

    public GameObject logInCanvas, mainMenuCanvas, JoinSessionCanvas, backButton;
    public GameObject JoinButton;
    public GameObject content;

    void Start () {

        JoinSessionCanvas.SetActive(false);
    }
		
	// Update is called once per frame
	void Update () {
		
	}

    public void GoBack() {

        mainMenuCanvas.SetActive(true);
        JoinSessionCanvas.SetActive(false);
    }

	public void RefreshSessions() {

		GetSessions ();
	}

    private void AddButtons(string[][] sessionList) {

        for (var i = content.transform.childCount - 1; i >= 1; i--)
        {

            var oldButton = content.transform.GetChild(i);
            oldButton.transform.parent = null;

        }

        int count = 0;
		if(sessionList != null){
        	for (int i = 0; i < sessionList.Length; i++) {
				
				GameObject newButton = (GameObject)Instantiate (JoinButton);
				newButton.GetComponent<JoinSessionButtonPrefab> ().SetUp (sessionList [i] [0], sessionList [i] [1]);

				Vector3 scale = JoinSessionCanvas.transform.lossyScale;  //die und folgende Zeile war eine schwere Geburt ...
				newButton.transform.localScale = scale;     
				newButton.transform.SetParent (content.transform);
				Debug.Log ("sessionList.Length=" + sessionList.Length + " ; sessionList[i].Length=" + sessionList [i].Length);
				Debug.Log ("sessionList[i][0]=" + sessionList [i][0] + " ; sessionList[i][1]=" + sessionList [i][1]);
				if (sessionList [i][1].Equals ("_")) {
					newButton.GetComponent<Image> ().enabled = false;
				} 
				count = i;
        	}
        	RectTransform newRT = content.GetComponent<RectTransform>();        //to get the RectTransform from content
        	newRT.sizeDelta = new Vector2(0, count * 100);                      //and strech it to fit all Buttons
		}
    }

    public void GetSessions() {
		
		GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().netwRout.TCPRequest(
            ListAllSessions,
            new string[] { "req", "userId" },
			new string[] { "getSessions", UserStatics.IdSelf.ToString() });
    }

    private void ListAllSessions(string[][] response) {

        string ret = "";

        foreach (string[] pair in response) {

          	if (pair[0].Equals("sessions")) {
             	ret += pair[1];
           	}
        }
		if (!ret.Equals ("")) { 
			string pattern = @"//|--";
			string[] sessionsAndLeader = Regex.Split (ret.TrimEnd ('-'), pattern);
			string[][] sessionList = new string[sessionsAndLeader.Length / 2][];
			for (int j = 0; j < sessionList.Length; j++) {
				sessionList [j] = new string[2];
			}

			int i = 0;
			foreach (string element in sessionsAndLeader) {
				Debug.Log ("element=" + element);
				//if (sessionList.Length > 1 && sessionList [i / 2].Length > 1) {
					sessionList [i / 2] [i % 2] = element;
					Debug.Log("sessionList[i / 2] [i % 2]: " + (i / 2) + "," + (i % 2) + "=" + sessionList [i / 2] [i % 2]);
					i++;
				//}
			}
			AddButtons (sessionList);
		} else {
			AddButtons (null);
		}
    }
}

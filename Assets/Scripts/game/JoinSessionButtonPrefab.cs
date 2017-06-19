using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinSessionButtonPrefab : MonoBehaviour {

    public Button joinSessionButton;
    public Text sessionIDText;
    public Text leaderText;

	// Use this for initialization
	void Start () {

	}

    public void setUp (Text sessionID, Text leader) {
        sessionIDText = sessionID;
        leaderText = leader;
    }
	

}

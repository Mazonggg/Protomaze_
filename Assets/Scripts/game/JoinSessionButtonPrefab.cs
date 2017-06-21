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

    public void setUp (string sessionID, string leader) {
        sessionIDText.text = sessionID;
        leaderText.text = leader;
    }
	

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSession : MonoBehaviour {

    public GameObject logInCanvas, mainMenuCanvas, CreateSessionCanvas, backButton;

    void Start () {
        CreateSessionCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void goBack() {
        mainMenuCanvas.SetActive(true);
        CreateSessionCanvas.SetActive(false);
    }
}

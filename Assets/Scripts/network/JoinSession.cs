using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinSession : MonoBehaviour {

    public GameObject logInCanvas, mainMenuCanvas, JoinSessionCanvas, backButton;

    void Start () {
        JoinSessionCanvas.SetActive(false);
 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void goBack() {
        mainMenuCanvas.SetActive(true);
        JoinSessionCanvas.SetActive(false);
    }
}

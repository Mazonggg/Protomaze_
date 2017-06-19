using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinSession : MonoBehaviour {

    public GameObject logInCanvas, mainMenuCanvas, JoinSessionCanvas, backButton;
    public GameObject JoinButton;
    public GameObject content;

    void Start () {
        JoinSessionCanvas.SetActive(false);
        AddButtons();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void goBack() {
        mainMenuCanvas.SetActive(true);
        JoinSessionCanvas.SetActive(false);
    }


    private void AddButtons() {
        int count = 0;
        for (int i = 0; i < 20; i++) {
            GameObject newButton = (GameObject)Instantiate(JoinButton); 
            newButton.transform.SetParent(content.transform);
            count = i;
        }
        RectTransform newRT = content.GetComponent<RectTransform>();
        newRT.sizeDelta = new Vector2(0, count * 100);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	
	public GameObject resumeButton, quitButton, pauseMenuCanvas;
	public GameObject debugText;

	private bool gameRunning = false;

	void Start() {

		TogglePause (false);
	}


	void FixedUpdate(){
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			TogglePause (!gameRunning);
		}
	}

	public void Pause() {
		TogglePause (true);
	}

	public void Resume() {
		TogglePause (false);
	}

	public void Quit() {

		GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().netwRout.TCPRequest(
			StartMainMenu,
			new string[] { "req", "userId" },
			new string[] { "leaveSession", UserStatics.IdSelf.ToString() });
	}

	private void StartMainMenu(string[][] response){
		SceneManager.LoadScene ("Menu");
	}

	private void TogglePause(bool stop) {

		Debug.Log("TogglePause start: " + gameRunning);
		pauseMenuCanvas.SetActive(stop);
		gameRunning = stop;
		debugText.GetComponent<Text> ().text = "Game Running: " + gameRunning;

		Debug.Log("TogglePause end: " + gameRunning);
		// LOGIC TO RESUME GAME.
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	
	public GameObject resumeButton, quitButton, pauseMenuCanvas;
	public GameObject debugText, timerText;

	private bool gamePaused = false;

	void Start() {

		TogglePause (false);
	}


	void Update(){
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			TogglePause (!gamePaused);
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

		Debug.Log("TogglePause start: " + gamePaused);
		pauseMenuCanvas.SetActive(stop);
		gamePaused = stop;
		debugText.GetComponent<Text> ().text = "Game paused: " + gamePaused;

		Debug.Log("TogglePause end: " + gamePaused);
		Time.timeScale = (stop ? 0f : 1f);
		timerText.GetComponent<TimerScript> ().Count (!stop);
		// LOGIC TO RESUME GAME.
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
	
	private static int timer = 0;
	public static int Timer {
		set { timer = value; }
	}

	private float startTime;

	void Start() {
		startTime = Time.realtimeSinceStartup;
	}
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup - startTime < timer) {
			gameObject.GetComponent<Text> ().text = ConvertSeconds((int)(startTime - Time.realtimeSinceStartup + timer));
		} else {
			gameObject.GetComponent<Text> ().text = "Time over";
			gameObject.GetComponent<Text> ().color = Constants.secondaryColor;
		}
	}

	private string ConvertSeconds(int time) {
		return (time / 60 < 10 ? "0" : "") + (time / 60) + ":" + (time % 60 < 10 ? "0" : "") + (time % 60);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
	
	private static float timer = 0;
	public static float Timer {
		set { timer = value; }
	}
	private float startTime;
	private float pauseTime = 0;
	private bool isCounting = true;

	void Start() {
		startTime = Time.realtimeSinceStartup;
		pauseTime = Time.realtimeSinceStartup;
	}
	// Update is called once per frame
	void Update () {
		if (isCounting) {
			if (timer > Time.realtimeSinceStartup - startTime) {
				gameObject.GetComponent<Text> ().text = ConvertSeconds ((int)(startTime - Time.realtimeSinceStartup + timer));
			} else {
				gameObject.GetComponent<Text> ().text = "Time over";
				gameObject.GetComponent<Text> ().color = Constants.secondaryColor;
			}
		}
	}

	/// <summary>
	/// Converts the seconds to clock time.
	/// </summary>
	/// <returns>The seconds.</returns>
	/// <param name="time">Time.</param>
	private string ConvertSeconds(int time) {
		return (time / 60 < 10 ? "0" : "") + (time / 60) + ":" + (time % 60 < 10 ? "0" : "") + (time % 60);
	}

	/// <summary>
	/// Starts or stops counting of time left to finish level.
	/// </summary>
	/// <param name="continueCounting">If set to <c>true</c> continue counting.</param>
	public void Count(bool continueCounting){

		isCounting = continueCounting;
		if (continueCounting) {
			timer += Time.realtimeSinceStartup - pauseTime;
		} else {
			pauseTime = Time.realtimeSinceStartup;
		}
	}
}

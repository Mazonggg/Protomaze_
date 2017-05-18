using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.Networking;

public class LogInUser : MonoBehaviour {

	public GameObject inputName, inputPwd;
	public GameObject logInCanvas, mainMenuCanvas;

	public void LoginUser() {

		string name = inputName.GetComponent<InputField>().text;
		string pwd = Constants.NetworkRoutines.Md5Sum(inputPwd.GetComponent<InputField>().text);

		Constants.NetworkRoutines.TCPRequest(
			HandleLogin, 
			"loginUser",
			new string[] {"userName", "pwd"},
			new string[] {name, pwd});
	}
		
	private void HandleLogin (int userId, string name){

		logInCanvas.SetActive (false);
		mainMenuCanvas.SetActive (true);

		Constants.UserHandler.ThisUser.Id = userId;
		Constants.UserHandler.ThisUser.ObjectName = name;
	}
}

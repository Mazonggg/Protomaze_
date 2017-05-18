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
			new string[] {"req", "userName", "pwd"},
			new string[] {"loginUser", name, pwd});
	}
		
	private void HandleLogin (string[][] response){

		logInCanvas.SetActive (false);
		mainMenuCanvas.SetActive (true);

        int IdTmp = -1;

        foreach( string[] pair in response) {
            
            if (pair[0].Equals("userId")) {
                int.TryParse(pair[1], out IdTmp);
            }
        }

        Constants.UserHandler.ThisUser.Id = IdTmp;
		Constants.UserHandler.ThisUser.ObjectName = inputName.GetComponent<InputField>().text;
	}
}

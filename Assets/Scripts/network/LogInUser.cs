using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LogInUser : MonoBehaviour {

	public GameObject inputName, inputPwd;
	public GameObject logInCanvas, mainMenuCanvas;

	public void LoginUser() {

		string name = inputName.GetComponent<InputField>().text;
		string pwd = GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().netwRout.Md5Sum(inputPwd.GetComponent<InputField> ().text);

		GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().netwRout.TCPRequest(
			HandleLogin, 
			new string[] {"req", "userName", "pwd"},
			new string[] {"loginUser", name, pwd});
	}
		
	private void HandleLogin (string[][] response){

        foreach( string[] pair in response) {

			if (pair[0].Equals("userId")) {
				
                int IdTmp = -1;
				int.TryParse(pair[1], out IdTmp);
				logInCanvas.SetActive(false);
				mainMenuCanvas.SetActive(true);

				UserStatics.SetUserLoggedIn (IdTmp, inputName.GetComponent<InputField> ().text);
				//UserStatics.SetUserInfo(0, IdTmp, inputName.GetComponent<InputField>().text, "");
				//UserStatics.IdSelf = IdTmp;
				//GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.Id = IdTmp;
				//GameObject.Find(Constants.softwareModel).GetComponent<SoftwareModel>().UserHandler.ThisUser.ObjectName = inputName.GetComponent<InputField>().text;

                return;
            }
        }
	}
}

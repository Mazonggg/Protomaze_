using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using System.Net;
using System.Net.Sockets;



/*
 * Represents the interface between the unity-game and the functional model of the Software in itself.
 * Establishes Connection to server.
 * 
 * Contains function Md5Sum() from 'http://wiki.unity3d.com/index.php?title=MD5 opened on: 2017_04_26'.
 */
public class SoftwareModel_sich_170503: MonoBehaviour {

	private UserHandler userHandler;
	private UnityWebRequest connection;

	private static string serverError = "Error";

	// Use this for initialization
	void Start () {

		timeStamp = Time.realtimeSinceStartup;
		gameObject.AddComponent<User> ();
		userHandler = new UserHandler (gameObject.GetComponent<User>());
		//Debug.Log ("### USER ###");
		//Debug.Log (gameObject.GetComponent<User> ());

		StartCoroutine (Login ("Protofox", "Protofox"));
		//connection = new Connection(userHandler.Host, "UserName-Input-Field.value");
		//connection.Init(userHandler.Host, "UserName-Input-Field.value");
	}

	// Time since last call.
	private float timeStamp = 0;
	// Intervall between calls.
	private float statusIntervall = 5;
	// Update is called once per frame
	void Update () {
		// If intervall between timeStamps is reached, call 
		if (Time.realtimeSinceStartup - timeStamp > statusIntervall) {
			timeStamp = Time.realtimeSinceStartup;
			StartCoroutine (KeepConnection ());
		}
	}

	/*
	 * Sends "connected" status to server.
	 */
	private IEnumerator KeepConnection() {

		string loginRequest = "http://h2678361.stratoserver.net/keepconnection.php";
		using (connection = UnityWebRequest.Get (loginRequest)) {

			yield return connection.Send ();

			if (connection.isError) {
				Debug.Log("KeepConnection():Error: " + connection.error);
			}
			else {
				Debug.Log("KeepConnection():S: " + connection.downloadHandler.text);
			}
		}
		Debug.Log("Start(): " + userHandler.ThisUser.Id + "; " + userHandler.ThisUser.ObjectName);
	}

	private float startTime = 0;
	/*
	 * Logs User in on server.
	 */
	private IEnumerator Login(string userName, string pwd) {

		startTime = Time.realtimeSinceStartup;
		Debug.Log("######### START #########");
		string loginRequest = "http://h2678361.stratoserver.net/loginUser.php?userName=" + userName + "&pwd=" + Md5Sum(pwd);
		using (connection = UnityWebRequest.Get (loginRequest)) {

			yield return connection.Send ();

			if (connection.isError) {
				Debug.Log("Login():Error: " + connection.error);
			}
			else {
				string response = connection.downloadHandler.text;
				// Checks if the request responses with an error
				if (response.StartsWith (serverError)) {
					Debug.Log ("response: "+ response);
				} else {
					//userHandler.ThisUser.Id = int.Parse (response);
					//userHandler.ThisUser.ObjectName = userName;
				}
			}
		}
		//Debug.Log("Start(): " + userHandler.ThisUser.Id + "; " + userHandler.ThisUser.ObjectName);

		Debug.Log("######### STOP #########");
		Debug.Log(Time.realtimeSinceStartup - startTime);
	}


	/*
	 * Logs player out, when Game closed.
	 */
	void OnApplicationQuit () {

		StartCoroutine (Logout(userHandler.ThisUser.ObjectName));
		StartCoroutine (WaitForClose ());
	}
	/*
	 * Shuts down the connection to the server, including php-Session.
	 */
	private IEnumerator Logout(string userName) {

		userName = "Protofox";
		Debug.Log ("Logout(): " + userName);
		string logoutRequest = "http://h2678361.stratoserver.net/logoutUser.php?userName=" + userName;
		using (connection = UnityWebRequest.Get (logoutRequest)) {

			yield return connection.Send ();

			if (connection.isError) {
				Debug.Log("Logout():Error: " + connection.error);
			}
			else {
				
				Debug.Log ("Logout(): Success: " + connection.downloadHandler.text);
			}
		}
	}

	private IEnumerator WaitForClose()
	{
		Debug.Log("Time: " + Time.time);
		yield return new WaitForSeconds(5);
		Debug.Log("Time: " + Time.time);
	}

	/*
	 	 * Reconnects the user to server.
	 	 * return true, if successfully reconnected.
	 	 */
	public bool Reconnect (User usr){
		
		connection = UnityWebRequest.Get("http://h2678361.stratoserver.net/reconnectUser.php?userId=" + usr.Id);

		return true;
	}

	/*
	 * Generates Md5 hash.
	 * Source: http://wiki.unity3d.com/index.php?title=MD5 opened on: 2017_04_26
	 */
	private string Md5Sum(string strToEncrypt){
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}
}
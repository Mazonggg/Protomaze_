using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.Networking;

using System;

/// Contains function Md5Sum() from 'http://wiki.unity3d.com/index.php?title=MD5 opened on: 2017_04_26'.
public class NetworkRoutines : MonoBehaviour {

	private static string serverError = "Error:";
	private static string serverResponse = "Response:";
	private static string serverHint = "Hint:";
	private static string serverRequest = "http://h2678361.stratoserver.net/scripts/connection.php?";

	private UnityWebRequest connection;


	public void TCPRequest(Action<int, string> callback, string constt, string[] keys, string[] values) {

		Debug.Log ("Constt: " + constt);
		string request = SerializeRequest (serverRequest, constt, "=1&", GenerateParams(keys, values));
		StartCoroutine (MakeRequest(callback, request));
	}
	/// <summary>
	/// Processes and returns all tcp-requests to server.
	/// </summary>
	/// <returns>The request.</returns>
	/// <param name="param">Parameter.</param>
	private IEnumerator MakeRequest(Action<int, string> callback, string request) {
		
		Debug.Log ("MakeRequest: " + request);
		using (connection = UnityWebRequest.Get (request)) {

			yield return connection.Send ();

			if (connection.isError) {
				Debug.Log(serverError + connection.error);
			}
			else {
				string response = connection.downloadHandler.text;
				// Checks if the request responses with an error
				if (response.StartsWith (serverError)) {
					Debug.Log (serverError + response);
				} else {
					Debug.Log (serverResponse + response);
					callback (0, response);
				}
			}
		}
	}

	private string SerializeRequest(params string[] parts) {
		string serial = "";

		foreach (string part in parts) {
			serial += part;
		}
		return serial;
	}

	/// <summary>
	/// Generates the parameters for a php request.
	/// </summary>
	/// <returns>The parameters.</returns>
	/// <param name="pars">Key-Value pairs for request.</param>
	private string GenerateParams(string[] keys, string[] values) {

		string gen = "?";

		for (int i = 0; i < keys.Length; i++) {
			gen += keys[i] + "=" + values[i] + "&";
		}
		return gen.Substring (gen.Length - 2);
	}



	/// <summary>
	/// Applies Md5s encryption to the string.
	/// </summary>
	/// <returns>The sum.</returns>
	/// <param name="strToEncrypt">String to encrypt.</param>
	public string Md5Sum(string strToEncrypt){
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHandler {

	private List<User> users = new List<User>();
	public User ThisUser {
		get { 
			if (users.Count > 0) {
				return users [0]; 
			} else {
				return null;
			}
		}
	}
	/*
	 * Creates object, that handles interaction etc. of users.
	 */
	public UserHandler(User usr) {

		Debug.Log ("UserHandler(): " + usr);
		users.Add (usr);
		Debug.Log ("UserHandler: loaded: " + users[0]);
		Debug.Log ("UserHandler: loaded: " + ThisUser);
	}
}

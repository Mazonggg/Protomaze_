﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the users in direct relation to the unity-engine.
/// Handles creation of GameObjects representing the users in a game-session.
/// </summary>
public class UserController: MonoBehaviour {

	public GameObject userPrefab;

	//int counter = 0;
	private List<User> users = new List<User>();
	public User ThisUser {
		get { 
			//counter++;
			//string userLog = "///// idSelf=" + UserStatics.IdSelf + " ///// ";
			int uid = -1;
			for (int i = 0; i < users.Count; i++) {
				uid = UserStatics.GetUserId (i);
				// userLog += "i=" + i + " ; id=" + uid + " ; name=" + UserStatics.GetUserName(uid) + " ; ref=" + UserStatics.GetUserRef(uid) + " /// ";
				if (uid == UserStatics.IdSelf) {
					// Debug.Log (counter + "UserController.ThisUser= " + userLog);
					return users [i]; 
				}
			}
			return null;
		}
	}

	/// <summary>
	/// Gets the user count in current session.
	/// </summary>
	/// <value>The user count.</value>
	public int UserCount {
		get { return users.Count; }
	}

	/// <summary>
	/// Gets the identifier of the user in database.
	/// </summary>
	/// <returns>The identifier.</returns>
	/// <param name="usr">Usr.</param>
	public int GetIndex(User usr) {

		return users.IndexOf (usr);
	}

	/// <summary>
	/// Adds the user to the gamescene and this handlers list.
	/// </summary>
	/// <param name="usr">Usr.</param>
	/*public void AddUser(User usr) {

		if (users.Count < 4) {
			users.Add (usr);
		}
	}*/

	/// <summary>
	/// Adds the user to the gamescene and this handlers list.
	/// </summary>
	/// <param name="user_ref">User reference in session (a, b, c or d).</param>
	/// <param name="user_id">User identifier in database.</param>
	/// <param name="user_name">User name.</param>
	public void AddUser(string user_ref, int user_id, string user_name) {

		if (users.Count < 4) {
			GameObject usr = GameObject.Instantiate(
				userPrefab, 
				StartLevel_1.GetStartPosition(users.Count), 
				Quaternion.Euler(0, 0, 0), 
				gameObject.transform);
			users.Add (usr.GetComponent<User> ());
			UserStatics.SetUserInfo(users.IndexOf(usr.GetComponent<User> ()),user_id, user_name, user_ref); 
			if (UserStatics.IsMySelf(users.IndexOf(usr.GetComponent<User> ()))) {
				usr.GetComponent<User> ().IsPlayed = true;
			}
			usr.GetComponent<User>().userInfo.GetComponent<TextMesh>().text = user_ref + " : " + user_name;
		}
	}

	/// <summary>
	/// Updates the user.
	/// </summary>
	/// <param name="user_update">User update.</param>
	public void UpdateUser (UpdateData user_update) {

		/*string stat = "/// idSelf=" + UserStatics.IdSelf + " /// ";
		for (int i = 0; i<users.Count; i++) {
			stat += "i=" + i + " ; isPlayed=" + users[i].IsPlayed + " ; updated=" + users[i].Updated + " /// ";
		}
		Debug.Log (stat);*/


		// Debug.Log ("UpdateUser: " + user_update.Id + " pos: " + user_update.Position.x + "," +user_update.Position.y + "," + user_update.Position.z );
		for (int i = 0; i < users.Count; i++) {
			if (UserStatics.GetUserId (i) == user_update.Id) {
				users [i].UpdateData = user_update;
			}
		}
	}

	private int counter = 0;
	void FixedUpdate() {

		//Debug.Log (counter++ + ". ThisUser.Updated=" + ThisUser.Updated);
	}
}

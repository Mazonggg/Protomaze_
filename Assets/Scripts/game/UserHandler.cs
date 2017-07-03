using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHandler: MonoBehaviour {

	public GameObject userPrefab;

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

		//Debug.Log ("AddUser");
		if (users.Count < 4) {
			GameObject usr = GameObject.Instantiate(userPrefab);
			users.Add (usr.GetComponent<User> ());
			Constants.SetUserInfo(users.IndexOf(usr.GetComponent<User> ()),user_id, user_name, user_ref); 
			if (Constants.IsMySelf(users.IndexOf(usr.GetComponent<User> ()))) {
				usr.GetComponent<User> ().IsPlayed = true;
			}
			usr.GetComponent<User>().userInfo.GetComponent<TextMesh>().text = user_id + " : " + user_ref + " : " + user_name;
		}
	}

	/// <summary>
	/// Updates the users.
	/// </summary>
	/// <param name="user_updates">User updates.</param>
	public void UpdateUsers (UpdateData[] user_updates) {

		foreach(User user in users) {
			foreach (UpdateData user_update in user_updates) {
				if (user.Id == user_update.Id) {
					user.UpdateData = user_update;
				}
			}
		}
	}
}

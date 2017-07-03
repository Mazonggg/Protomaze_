using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Constants {

	// Holds all types of GObject forms.
	public static string[] objectForms = {"Cube", "Cone", "Ball", "Sphere"};

	public static Color userColor = new Color (0.25f, 0.5f, 0.9f);
	// Movement speed of Users.
	public static float moveSpeed = 10f;
	public static string softwareModel = "SoftwareModel";

	// Store brief information of users in game.
	// Necassary for change between gamescenes.
	private static int idSelf = -1;
	public static int IdSelf {
		get { return idSelf; }
		set { idSelf = value; }
	}
	private static int[] userIds = {-1, -1, -1, -1};
	private static string[] userNames = {"", "", "", ""};
	private static string[] userRefs = {"", "", "", ""};

	/// <summary>
	/// Gets the user identifier.
	/// </summary>
	/// <returns>The user identifier.</returns>
	/// <param name="index_in_game">Index in game.</param>
	public static int GetUserId(int index_in_game) {

		return userIds [index_in_game];
	}

	/// <summary>
	/// Determines if the given index of the player in the game represents myself.
	/// </summary>
	/// <returns><c>true</c> if is my self the specified index_in_game; otherwise, <c>false</c>.</returns>
	/// <param name="index_in_game">Index in game.</param>
	public static bool IsMySelf(int index_in_game){

		return idSelf == userIds [index_in_game];
	}

	/// <summary>
	/// Gets the name of the user for a given user_id.
	/// </summary>
	/// <returns>The user name.</returns>
	/// <param name="user_id">User identifier.</param>
	public static string GetUserName (int user_id) {

		for (int i = 0; i < userIds.Length; i++) {
			if (userIds [i] == user_id) {
				return userNames [i];
			}
		}
		return "";
	}

	/// <summary>
	/// Gets the user ref of the user for a given user_id.
	/// </summary>
	/// <returns>The user ref.</returns>
	/// <param name="user_id">User identifier.</param>
	public static string GetUserRef (int user_id) {

		for (int i = 0; i < userIds.Length; i++) {
			if (userIds [i] == user_id) {
				return userRefs [i];
			}
		}
		return "";
	}

	/// <summary>
	/// Sets the name of the user in reference to his/her Id.
	/// </summary>
	/// <returns><c>true</c>, if user name was set, <c>false</c> otherwise.</returns>
	/// <param name="user_id">User identifier.</param>
	/// <param name="user_name">User name.</param>
	public static void SetUserInfo(int index_of_user, int user_id, string user_name, string user_ref) {

		userIds [index_of_user] = user_id;
		userNames [index_of_user] = user_name;
		userRefs [index_of_user] = user_ref;
	}

	/// <summary>
	/// Id of session in database.
	/// </summary>
	public static int sessionId = 0;
}

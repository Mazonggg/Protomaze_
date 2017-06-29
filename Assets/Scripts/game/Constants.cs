using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Constants {

	// Holds all types of GObject forms.
	public static string[] objectForms = {"Cube", "Cone", "Ball", "Sphere"};

	// Movement speed of Users.
	public static float moveSpeed = 10f;
	public static string softwareModel = "SoftwareModel";


	public static int[] userIds = {0, 0, 0, 0};
	public static string[] userNames = {"", "", "", ""};
	public static int sessionId = 0;
}

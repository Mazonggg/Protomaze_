using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Constants {

	// Holds all types of GObject forms.
	public static string[] objectForms = {"Cube", "Cone", "Ball", "Sphere"};

	// Movement speed of Users.
	public static float moveSpeed = 10f;

	public static UserHandler UserHandler;
	public static NetworkRoutines NetworkRoutines;
}

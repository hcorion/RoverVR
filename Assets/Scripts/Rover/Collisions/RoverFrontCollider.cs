using UnityEngine;
using System.Collections;

public class RoverFrontCollider : MonoBehaviour
{
	public static bool canMoveForward = true;

	void OnTriggerEnter (Collider c)
	{
		canMoveForward = false;
	}

	void OnTriggerExit (Collider c)
	{
		canMoveForward = true;
	}
}

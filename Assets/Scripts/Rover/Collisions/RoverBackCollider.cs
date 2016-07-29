using UnityEngine;
using System.Collections;

public class RoverBackCollider : MonoBehaviour
{
	public static bool canMoveBackward = true;

	void OnTriggerEnter (Collider c)
	{
		canMoveBackward = false;
	}

	void OnTriggerExit (Collider c)
	{
		canMoveBackward = true;
	}
}

using UnityEngine;
using System.Collections;

public class RoverBackCollider : MonoBehaviour
{
	public static bool canMoveBackwards = true;
	private float time;
	public float maxTime = 0;

	void OnTriggerStay (Collider c)
	{
		canMoveBackwards = false;
		time = 0;
		//Debug.Log("I can move forward!");
	}

	void Update()
	{
		time += Time.deltaTime;
		if (time > 0.4f)
		{
			canMoveBackwards = true;
		}
	}
}

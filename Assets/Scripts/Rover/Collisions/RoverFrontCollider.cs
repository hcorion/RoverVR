using UnityEngine;
using System.Collections;

public class RoverFrontCollider : MonoBehaviour
{
	public static bool canMoveForward = true;
	private float time;
	public float maxTime = 0;

	void OnTriggerStay (Collider c)
	{
		//Debug.Log("I've hit " + c);
		canMoveForward = false;
		time = 0;
		//Debug.Log("I can't move forward!");
	}

	void Update()
	{
		time += Time.deltaTime;
		if (time > 0.4f)
		{
			canMoveForward = true;
			//Debug.Log("I can move forward!");
		}
	}
}

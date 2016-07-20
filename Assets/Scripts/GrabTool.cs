using UnityEngine;
using System.Collections;

public class GrabTool : MonoBehaviour
{
	Vector3 origin;
	public float radius;
	public float maxDistance;

	void Start ()
	{
		origin = transform.position;
	}

	void Update ()
	{
		/*RaycastHit hitInfo;
		if (Physics.SphereCast (origin, radius, maxDistance, out hitInfo)) {
			//Attach Tool if Dropped
		}*/
	}
}

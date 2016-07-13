using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
	public void ShootLaserFromTargetPosition (LineRenderer lineRenderer, Vector3 targetPosition, Vector3 direction, float length, float width)
	{
		Ray ray = new Ray (targetPosition, direction);
		RaycastHit hit;
		Vector3 endPosition = targetPosition + (length * direction);

		if (Physics.Raycast (ray, out hit, length)) {
			endPosition = hit.point;
		}

		lineRenderer.SetPosition (0, targetPosition);
		lineRenderer.SetPosition (1, endPosition);
	}
}

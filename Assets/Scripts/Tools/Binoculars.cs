using UnityEngine;
using System.Collections;

namespace NewtonVR.Example
{
	public class Binoculars : NVRInteractableItem
	{
		public float maxDistance;
		public float minDistance;
		public float zoomSpeed;

		public Camera viewCamera;

		float distance = 5;
		float zoom = 0;

		void Update ()
		{
			viewCamera.fieldOfView = distance + zoom;
		}
	}
}

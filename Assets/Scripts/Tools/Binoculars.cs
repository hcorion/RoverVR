using UnityEngine;
using System.Collections;

namespace NewtonVR.Example
{
	public class Binoculars : NVRInteractableItem
	{

		public float distance;
		public GameObject viewCamera;

		void Update ()
		{
			Vector3 forward = transform.TransformVector (Vector3.back);
			Vector3 cameraPosition = transform.position + forward * distance;

			viewCamera.transform.position = cameraPosition;
		}
	}
}

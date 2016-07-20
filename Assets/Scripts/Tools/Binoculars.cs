using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NewtonVR.Example
{
	public class Binoculars : NVRInteractableItem
	{
		public float distance;
		public Camera viewCamera;

		void Update ()
		{
			viewCamera.fieldOfView = distance;
		}
	}
}

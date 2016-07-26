using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NewtonVR.Example
{
	public class Binoculars : NVRInteractableItem
	{
		public float distance;
		public Camera viewCamera;

		new void Start ()
		{
			base.Start ();
			viewCamera.fieldOfView = distance;
		}
	}
}

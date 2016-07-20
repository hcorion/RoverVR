using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NewtonVR.Example
{
	public class Binoculars : NVRInteractableItem
	{
		public float distance;
		public Camera viewCamera;
		public GameObject textObject;
		public GameObject origin;
		public string units;

		void Update ()
		{
			viewCamera.fieldOfView = distance;

			RaycastHit hit;
			Vector3 forward = transform.TransformVector (Vector3.back);
			Ray ray = new Ray (origin.transform.position, forward);

			if (Physics.Raycast (ray, out hit)) {
				textObject.GetComponent<Text> ().text = hit.distance.ToString ("F2") + units;
			}
		}
	}
}

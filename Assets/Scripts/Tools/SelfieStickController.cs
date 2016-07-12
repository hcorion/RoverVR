using UnityEngine;
using System.Collections;

namespace NewtonVR.Example
{
	public class SelfieStickController : NVRInteractableItem
	{
		public float scanDistance;
		public Transform selfieCamera;

		public override void UseButtonDown ()
		{
			print (isLife ());
		}

		string isLife ()
		{
			Vector3 fwd = selfieCamera.TransformDirection (Vector3.forward); 
			RaycastHit hit;
			Ray r = new Ray (selfieCamera.position, fwd);
			if (Physics.Raycast (r, out hit, scanDistance)) {
				Debug.DrawRay (selfieCamera.position, fwd, Color.red, 1, false);
				WaterSource waterSrc = hit.collider.GetComponent<WaterSource> ();
				if (waterSrc != null) {
					float moisture = waterSrc.moistureContent / Vector3.Distance (hit.point, hit.transform.position);
					Debug.DrawLine (hit.transform.position, hit.point, Color.blue, 20, false);
					if (moisture < 1) {
						return "You found life";
					} else {
						return "You are close";
					}
				} else {
					return "No life here";
				}
			} else {
				return "Use on soil";
			}
		}

		public override void UseButtonUp ()
		{

		}
	}
}
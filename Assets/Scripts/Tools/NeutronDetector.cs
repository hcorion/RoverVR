using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NewtonVR.Example
{
	public class NeutronDetector : NVRInteractableItem
	{
		public float scanDistance;
		public string units;
		public GameObject textObject;

		public LineRenderer lineRenderer;
		public float laserWidth;
		public LaserScript laserScript;

		Text content;

		new void Start ()
		{
			base.Start ();

			Vector3[] initLaserPositions = new Vector3[2] {
				Vector3.zero,
				Vector3.zero
			};

			//lineRenderer.SetPosition (initLaserPositions);
			lineRenderer.SetWidth (laserWidth, laserWidth);

			content = textObject.GetComponent<Text> ();
			content.text = "";
		}

		void Update ()
		{
			if (AttachedHand != null) {
				//Disable Hand Modle
			}
		}

		public override void UseButtonDown ()
		{
			base.UseButtonDown ();
			content.text = GetMoistureContent ();
			AttachedHand.TriggerHapticPulse (500);
		}

		string GetMoistureContent ()
		{
			RaycastHit hit;
			Vector3 forward = transform.TransformVector (Vector3.left);
			Ray ray = new Ray (transform.position, forward);
			if (Physics.Raycast (ray, out hit, scanDistance)) {
				Debug.DrawRay (transform.position, forward, Color.red, 1, false);
				WaterSource waterSrc = hit.collider.GetComponent<WaterSource> ();
				if (waterSrc != null) {
					float moisture = 1 / Vector3.Distance (hit.point, hit.transform.position);
					laserScript.ShootLaserFromTargetPosition (lineRenderer, transform.position, forward, scanDistance, laserWidth);
					Debug.DrawLine (hit.transform.position, hit.point, Color.blue, 20, false);
					if (moisture > 0) {
						return moisture.ToString ("F2") + units;
					} else {
						return ("None");
					}
				} else {
					return ("None");
				}
			} else {
				return ("None");
			}
		}
	}
}

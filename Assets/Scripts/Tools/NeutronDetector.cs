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

		Text content;
		bool buttonPressed;

		new void Start ()
		{
			base.Start ();

			buttonPressed = false;

			Vector3[] initLaserPositions = new Vector3[2] {
				Vector3.zero,
				Vector3.zero
			};

			lineRenderer.SetPositions (initLaserPositions);
			lineRenderer.SetWidth (laserWidth, laserWidth);

			content = textObject.GetComponent<Text> ();
			content.text = "";
		}

		void Update ()
		{
			if (buttonPressed) {
				content.text = GetMoistureContent ();
			}

			if (AttachedHand != null) {
				//Disable Hand Modle
			}
		}

		public override void UseButtonDown ()
		{
			base.UseButtonDown ();
			buttonPressed = true;
		}

		public override void UseButtonUp ()
		{
			base.UseButtonUp ();
			buttonPressed = false;
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
					ShootLaserFromTargetPosition (transform.position, forward, scanDistance);
					lineRenderer.enabled = true;
					Debug.DrawLine (hit.transform.position, hit.point, Color.green, 20, false);
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

		public void ShootLaserFromTargetPosition (Vector3 targetPosition, Vector3 direction, float length)
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
}

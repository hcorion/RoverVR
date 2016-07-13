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
		public Transform body;

		public float rayOffSetX;
		public float rayOffSetY;
		public float rayOffSetZ;

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

			if (!buttonPressed) {
				lineRenderer.enabled = false;
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
			Vector3 targetPosition = new Vector3 (body.position.x + rayOffSetX, body.position.y + rayOffSetY, body.position.z + rayOffSetZ);
			Ray ray = new Ray (targetPosition, forward);
			ShootLaserFromTargetPosition (targetPosition, forward, scanDistance);
			lineRenderer.enabled = true;
			if (Physics.Raycast (ray, out hit, scanDistance)) {
				Debug.DrawRay (body.position, forward, Color.red, 1, false);
				WaterSource waterSrc = hit.collider.GetComponent<WaterSource> ();
				if (waterSrc != null) {
					float moisture = 1 / Vector3.Distance (hit.point, hit.transform.position);
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
			targetPosition = new Vector3 (targetPosition.x + rayOffSetX, targetPosition.y + rayOffSetY, targetPosition.z + rayOffSetZ);
			Ray ray = new Ray (targetPosition, direction);
			RaycastHit hit;
			Vector3 endPosition = targetPosition + (length * direction);

			if (Physics.Raycast (ray, out hit, length)) {
				endPosition = hit.point;
			}

			lineRenderer.SetColors (Color.blue, Color.blue);
			lineRenderer.SetPosition (0, targetPosition);
			lineRenderer.SetPosition (1, endPosition);
		}
	}
}

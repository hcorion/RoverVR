﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NewtonVR
{
	public class NeutronDetector : NVRInteractableItem
	{
		public float scanDistance;
		public string units;
		public GameObject textObject;
		public Transform firePoint;

		public LineRenderer lineRenderer;
		public float laserWidth;

		Text content;
		bool buttonPressed;

		public DamageUI dmgUI;
		public GameObject damagecCanvas;
		public float healthLossRate;

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

			dmgUI = GetComponent <DamageUI> ();
			damagecCanvas.SetActive (false);
		}

		new void Update ()
		{
            base.Update();
			if (1 > 0f) {
				if (buttonPressed) {
					//To make the battery go down by time, not framerate.
					//dmgUI.health -= Time.deltaTime * healthLossRate;
					content.text = GetMoistureContent ();
				}

				if (!buttonPressed) {
					lineRenderer.enabled = false;
				}

				if (AttachedHand != null) {
					//damagecCanvas.SetActive (true);
				}

				if (AttachedHand == null) {
					buttonPressed = false;
					lineRenderer.enabled = false;

					//damagecCanvas.SetActive (false);
				}
			} else {
				lineRenderer.enabled = false;
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
			Ray ray = new Ray (firePoint.position, forward);
			ShootLaserFromTargetPosition (firePoint.position, forward, scanDistance);
			lineRenderer.enabled = true;
			if (Physics.Raycast (ray, out hit, scanDistance)) {
				Debug.DrawRay (firePoint.position, forward, Color.red, 0.01f, false);
				WaterSource waterSrc = hit.collider.GetComponent<WaterSource> ();
				if (waterSrc != null) {
					float moisture = 1 / Vector3.Distance (hit.point, hit.transform.position);
					Debug.DrawLine (hit.transform.position, hit.point, Color.blue, 20f, false);
					if (moisture >= 0) {
						content.fontSize = 50;
						return moisture.ToString ("F2") + units;
					} else {
						content.fontSize = 50;
						return ("0" + units);
					}
				} else {
					content.fontSize = 35;
					return ("Object Contains No Moisture");
				}
			} else {
				content.fontSize = 35;
				return ("No Soil Found");
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

			lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));

			lineRenderer.SetColors (Color.red, Color.red);
			lineRenderer.SetPosition (0, targetPosition);
			lineRenderer.SetPosition (1, endPosition);
		}
	}
}

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NewtonVR.Example
{
	public class SelfieStickController : NVRInteractableItem
	{
		public float scanDistance;
		public Transform selfieCamera;
		private GameObject gameController;
		public Sprite[] noLifeImages;
		public Sprite[] LifeImages;

		public float waitTime = 0.0f;
		
		public Image CameraScreen;
		private float curTime = 0.0f;

		new void Start ()
		{
			base.Start ();
			gameController = GameObject.FindGameObjectWithTag ("GameController");
			if (noLifeImages[0] == null || LifeImages[0] == null)
			{
				Debug.LogWarning("NoLifeImages or LifeImages has no images assigned!");
			}
		}

		void Update()
		{
			curTime += Time.deltaTime;
		}

		public override void UseButtonDown ()
		{
			if(curTime >= waitTime)
			{
				curTime = 0.0f;
				print (isLife ());
			}
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
					float moisture = 1 / Vector3.Distance (hit.point, hit.transform.position);
					Debug.DrawLine (hit.transform.position, hit.point, Color.blue, 20, false);
					if (moisture < 1) {
						return "Life found";
						gameController.GetComponent<GameManager> ().GameOver ();
						CameraScreen.sprite = LifeImages[Random.Range(0, LifeImages.Length)];
					} else {
						return "You are close";
						CameraScreen.sprite = noLifeImages[Random.Range(0, noLifeImages.Length)];
					}
				} else {
					return "No life here";
					CameraScreen.sprite = noLifeImages[Random.Range(0, noLifeImages.Length)];
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
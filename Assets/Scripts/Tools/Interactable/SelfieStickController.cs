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
		public GameObject loadingScreen;

		public Sprite blankImage;

		private bool isLoading = false;

		private WaterSource waterSrc;

		DamageUI dmgUI;
		public GameObject canvas;
		public float healthLossRate;

		//Used for the GameManager Script
		private int wonTimes = 0;

		new void Start ()
		{
			base.Start ();
			gameController = GameObject.FindGameObjectWithTag ("GameController");
			if (noLifeImages [0] == null || LifeImages [0] == null) {
				Debug.LogWarning ("NoLifeImages or LifeImages has no images assigned!");
			}

			dmgUI = GetComponent<DamageUI> ();
			canvas.SetActive (false);
		}

		new void Update ()
		{
			base.Update ();
			if (AttachedHand == null) {
				canvas.SetActive (false);
			} else if (AttachedHand != null) {
				canvas.SetActive (true);
			}
			if (wonTimes == 1) {
				gameController.GetComponent<GameManager> ().wonFirstState ();
			}
		}

		public override void UseButtonDown ()
		{
			if (!isLoading && dmgUI.health > 0f) {
				GetComponent<AudioSource> ().Play ();
				print ("Selfie Stick Condition: " + +dmgUI.health);
				print (isLife ());
			}
			
		}

		string isLife ()
		{
			dmgUI.health -= healthLossRate;

			GetComponent<AudioSource> ().Play ();
			Vector3 fwd = selfieCamera.TransformDirection (Vector3.forward); 
			RaycastHit hit;
			Ray r = new Ray (selfieCamera.position, fwd);

			if (Physics.Raycast (r, out hit, scanDistance)) {
				
				Debug.DrawRay (selfieCamera.position, fwd, Color.yellow, 1, false);
				WaterSource waterSrc = hit.collider.GetComponent<WaterSource> ();

				if (waterSrc != null) {
					
					float moisture = 1 / Vector3.Distance (hit.point, hit.transform.position);

					if (moisture >= waterSrc.minWaterForLife) {
						StartCoroutine (loadOnScreen (LifeImages [Random.Range (0, LifeImages.Length)], true));
						Debug.DrawLine (hit.transform.position, hit.point, Color.green, 20, false);
						wonTimes++;
						return "Life found";
					} else {
						StartCoroutine (loadOnScreen (noLifeImages [Random.Range (0, noLifeImages.Length)], false));
						Debug.DrawLine (hit.transform.position, hit.point, Color.red, 20, false);
						return "You are close";
					}
				} else {
					StartCoroutine (loadOnScreen (noLifeImages [Random.Range (0, noLifeImages.Length)], false));
					Debug.DrawLine (hit.transform.position, hit.point, Color.red, 20, false);
					return "No life here";
				}
			} else {
				Debug.DrawLine (hit.transform.position, hit.point, Color.red, 20, false);
				return "Use on soil";
			}
		}

		IEnumerator loadOnScreen (Sprite image, bool hasWon)
		{
			isLoading = true;
			CameraScreen.sprite = blankImage;
			loadingScreen.SetActive (true);
			yield return new WaitForSeconds (waitTime);
			loadingScreen.SetActive (false);
			CameraScreen.sprite = image;
			if (hasWon == true) {
				gameController.GetComponent<GameManager> ().GameOver ();
			}
			isLoading = false;
			yield return null;
		}

	}
}
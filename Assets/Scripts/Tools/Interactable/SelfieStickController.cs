using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NewtonVR
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

		public DamageUI dmgUI;
		public GameObject damageCanvas;
		public float healthLossRate;

		public GameObject canvasUI;
		public GameObject textObjectNumber;
		public GameObject textObject;
		public GameObject panelObject;
		Text lifeNumber;
		Text lifeStatus;
		Image panel;

		//Used for the GameManager Script
		private int wonTimes = 0;
		public int totalLife;

		new void Start ()
		{
			base.Start ();
			gameController = GameObject.FindGameObjectWithTag ("GameController");
			if (noLifeImages [0] == null || LifeImages [0] == null) {
				Debug.LogWarning ("NoLifeImages or LifeImages has no images assigned!");
			}

			dmgUI = GetComponent<DamageUI> ();
			damageCanvas.SetActive (false);

			lifeStatus = textObject.GetComponent<Text> ();
			lifeNumber = textObjectNumber.GetComponent<Text> ();
			panel = panelObject.GetComponent<Image> ();
			canvasUI.SetActive (false);
		}

		new void Update ()
		{
			base.Update ();
			if (AttachedHand == null) {
				damageCanvas.SetActive (false);
			} else if (AttachedHand != null) {
				damageCanvas.SetActive (true);
			}
			if (wonTimes == 1) {
				gameController.GetComponent<GameManager> ().wonFirstState ();
			}
			if (wonTimes == 6) {
				gameController.GetComponent<GameManager> ().wonGame ();
			}
		}

		public override void UseButtonDown ()
		{
			if (!isLoading && dmgUI.health > 0f) {
				//print ("Selfie Stick Condition: " + dmgUI.health);
				isLife ();
			}
			
		}

		string isLife ()
		{
			Vector3 fwd = selfieCamera.TransformDirection (Vector3.forward); 
			RaycastHit hit;
			Ray r = new Ray (selfieCamera.position, fwd);

			if (Physics.Raycast (r, out hit, scanDistance)) {
				//dmgUI.health -= healthLossRate;

				Debug.DrawRay (selfieCamera.position, fwd, Color.yellow, 1, false);
				WaterSource waterSrc = hit.collider.GetComponent<WaterSource> ();

				if (waterSrc != null) {
					
					float moisture = 1 / Vector3.Distance (hit.point, hit.transform.position);

					if (moisture >= waterSrc.minWaterForLife) {
						Debug.DrawLine (hit.transform.position, hit.point, Color.green, 20, false);

						if (hit.transform.gameObject.GetComponent<WaterSource> ().found == false) {
							hit.transform.gameObject.GetComponent<WaterSource> ().found = true;
							
							wonTimes++;
						}

						StartCoroutine (loadOnScreen (LifeImages [Random.Range (0, LifeImages.Length)], true));

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

			StartCoroutine (loadLifeUI (hasWon));

			yield return null;
		}

		IEnumerator loadLifeUI (bool life)
		{
			if (life) {
				wonTimes++;
				lifeNumber.text = wonTimes + " of " + totalLife;
				panel.color = Color.green;
				lifeStatus.text = "Life Found:";
			} else {
				panel.color = Color.red;
				lifeNumber.text = "";
				lifeStatus.text = "Life Not Found";
			}

			canvasUI.SetActive (true);
			yield return new WaitForSeconds (waitTime);
			canvasUI.SetActive (false);
			yield return null;
		}
	}
}

using UnityEngine;
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

		new void Start ()
		{
			base.Start ();
			gameController = GameObject.FindGameObjectWithTag ("GameController");
			if (noLifeImages [0] == null || LifeImages [0] == null) {
				Debug.LogWarning ("NoLifeImages or LifeImages has no images assigned!");
			}
		}

		void Update ()
		{

		}

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
					float moisture = 1 / Vector3.Distance (hit.point, hit.transform.position);
					Debug.DrawLine (hit.transform.position, hit.point, Color.green, 20, false);
					if (moisture < 1) {
						gameController.GetComponent<GameManager> ().GameOver ();
						StartCoroutine (loadOnScreen (LifeImages [Random.Range (0, LifeImages.Length)]));
						return "Life found";
					} else {
						StartCoroutine (loadOnScreen (noLifeImages [Random.Range (0, noLifeImages.Length)]));
						return "You are close";
					}
				} else {
					StartCoroutine (loadOnScreen (noLifeImages [Random.Range (0, noLifeImages.Length)]));
					return "No life here";
				}
			} else {
				return "Use on soil";
			}
		}

		public override void UseButtonUp ()
		{

		}

		IEnumerator loadOnScreen (Sprite image)
		{
			CameraScreen.sprite = blankImage;
			loadingScreen.SetActive (true);
			yield return new WaitForSeconds (waitTime);
			loadingScreen.SetActive (false);
			CameraScreen.sprite = image;
			yield return null;
		}

	}
}
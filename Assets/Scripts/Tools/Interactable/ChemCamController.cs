using UnityEngine;
using System.Collections;

namespace NewtonVR
{
	public class ChemCamController : NVRInteractableItem
	{
		public Transform shootPoint;
		#if UNITY_EDITOR_OSX
		private bool buttonDown = true;
		#else
		private bool buttonDown = false;
		#endif

		//Is used to tell if the user has changed and is now pointing at a different rock.
		private GameObject lastRock;
		public GameObject laser;
		private Vector3 laserIntialPosition;
		public Material laserMat;
		public GameObject lightGameObject;
		//For the manipulation of the color of light.
		new public Light light;
		private Color32 lightColourToLerp;
		private Color32 previousColour;
		private float currentTime;
		//For breaking the rocks
		private float breakTime;
		float rockbreakage = 0.0f;

		//Used for lerping sound.
		private bool doLerpSound = false;
		private AudioSource sfx;
		private float currentLerpTime = 0;

		//Used for indicating damage to the player.
		public DamageUI dmgUI;
		public GameObject canvas;
		public float healthLossRate;

		public float laserScale = 50f;

		private Light rockLight;
		public GameObject rockLightPrefab;

		// Use this for initialization
		new void Start ()
		{
			base.Start ();
			laserIntialPosition = laser.transform.localPosition;
			//laserMat = laser.GetComponent<Renderer>().material;
			//light = lightGameObject.GetComponent<Light>();
			previousColour = light.color;
			sfx = GetComponent<AudioSource> ();

			dmgUI = GetComponent<DamageUI> ();
			canvas.SetActive (false);

			//Debug.Log ("The sfx gameobject is equal to: " + sfx);
			sfx.Play ();
		}

		new void Update ()
		{
			base.Update ();

			if (AttachedHand == null) {
				//buttonDown = false;
				canvas.SetActive (false);
			} else if (AttachedHand != null) {
				canvas.SetActive (true);
			}

			lerpSound ();
			if (buttonDown == true && dmgUI.health > 0f) {
				//Lerping Color.
				lerpColor ();

				RaycastHit hit;
				Vector3 forward = transform.TransformVector (Vector3.left);
				bool raycast = Physics.Raycast (shootPoint.position, forward, out hit);

				//Updating the position of the laser and the light
				laser.transform.localPosition = new Vector3 ((-hit.distance) / 2 + shootPoint.transform.localPosition.x, laser.transform.localPosition.y, laser.transform.localPosition.z);
				laser.transform.localScale = new Vector3 (laser.transform.localScale.x, hit.distance / transform.root.localScale.y * laserScale, laser.transform.localScale.z);
				lightGameObject.transform.position = new Vector3 (hit.point.x, hit.point.y, hit.point.z) + transform.right / 9.0f;

				if (raycast && hit.distance <= 3) {
					//Dealing damage
                    //Commenting this out because when you build the game the dmgUI is set to 0.
					//dmgUI.health -= Time.deltaTime * healthLossRate;

					ObjectProperties objectProperties = hit.transform.GetComponent<ObjectProperties> ();
					if (objectProperties != null) {
						rockLight = hit.transform.GetComponentInChildren<Light> ();
						//Debug.Log(rockLight);
						string rockMaterial = objectProperties.getSimpleMaterial ();
                        //Debug.Log ("The current material is:" + rockMaterial);
						if (lastRock == hit.transform.gameObject) {
							//If we're still on the same rock.
							if (rockbreakage != 0.0f) {
								breakTime += Time.deltaTime;

								if (rockLight != null) {
									rockLight.intensity += Time.deltaTime * 16f;
								} else if (objectProperties.isRockBreak == false){
									GameObject newLight = (GameObject)GameObject.Instantiate(rockLightPrefab, hit.transform.position, Quaternion.identity);
									newLight.transform.parent = hit.transform.root;
									rockLight = newLight.GetComponent<Light>();
									//print ("Object does not contain a point light");
								}

								if (breakTime >= rockbreakage) {
									//Debug.Log ("Rock has been broken");
									objectProperties.breakRock ();
								}
							} else {
								//Debug.Log ("Rock is unbreakable");
							}
                                
						} else {
							//If 
							rockbreakage = objectProperties.getSimpleRockBreakage ();
							breakTime = 0;

							if (lastRock == null) {
								//
								lastRock = hit.transform.gameObject;
								//If we haven't hit anything yet.
								laser.SetActive (true);
							}
							switch (rockMaterial) {
							case "Unusable":
								lightColourToLerp = new Color32 (255, 0, 0, 255);
								break;
							case "Aluminum":
								lightColourToLerp = new Color32 (76, 88, 156, 255);
								break;
							case "Copper":
								lightColourToLerp = new Color32 (111, 181, 109, 255);
								break;
							default:
								{
									Debug.LogError ("Woops, the material " + rockMaterial + " is not recognized by the ChemCamController Script.");
									lightColourToLerp = new Color32 (255, 0, 0, 255);
									break;
								}
							}
						}
					} else {
						//Debug.Log ("This object has no ObjectProperties script.");
						lastRock = null;
					}
				} else {
					//Debug.Log ("The ChemCam didn't hit anything. Move closer or something isn't working.");
					lightColourToLerp = new Color32 (255, 0, 0, 255);
				}
			} else if (dmgUI.health <= 0f) {
				laser.SetActive (false);
				lightGameObject.SetActive (false);
			}
		}

		public override void UseButtonDown ()
		{
			buttonDown = true;
			laser.SetActive (true);
			lightGameObject.SetActive (true);
			doLerpSound = true;
			sfx.Play ();
		}

		public override void UseButtonUp ()
		{
			buttonDown = false;
			lastRock = null;
			laser.SetActive (false);
			lightGameObject.SetActive (false);
			doLerpSound = false;
			sfx.volume = 0;
		}

		private void lerpColor ()
		{
			//Debug.Log("TEST");
			//Debug.Log (light);
			//Debug.Log (lightColourToLerp);
			const float lerpTime = 2;
			if (currentTime / lerpTime < 1.0) {
				//If we haven't acheived the goal.
				currentTime += Time.deltaTime;
				light.color = Color32.Lerp (previousColour, lightColourToLerp, currentTime / lerpTime);
				laserMat.color = Color32.Lerp (previousColour, lightColourToLerp, currentTime / lerpTime);
				laserMat.SetColor ("_EmissionColor", laserMat.color);
			} else if (lightColourToLerp != light.color) {
				//If we're changing color
				currentTime = 0.0f;
				previousColour = light.color;
			}
		}

		private void lerpSound ()
		{
			const float lerpTime = 3;
			if (doLerpSound == true && currentLerpTime / lerpTime < 1.0f) {
				//If we're lerping.
				//Debug.Log ("Lerping Sound! The sfx gameobject is " + sfx);
				currentLerpTime += Time.deltaTime;
				sfx.volume = Mathf.Lerp (0f, 1f, currentLerpTime / lerpTime);
			} else if (currentLerpTime / lerpTime > 1.0f) {
				//If we're supposed to be lerping but we can't yet.
				doLerpSound = false;
				currentLerpTime = 0;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

namespace NewtonVR
{
	public class ChemCamController : NVRInteractableItem
	{
		public Transform shootPoint;
		private bool buttonDown = true;
		//Is used to tell if the user has changed and is now pointing at a different rock.
		private GameObject lastRock;
		public GameObject laser;
		private Vector3 laserIntialPosition;
		public Material laserMat;
		public GameObject lightGameObject;
		//For the manipulation of the color of light.
		public Light light;
		private Color32 lightColourToLerp;
		private Color32 previousColour;
		private float currentTime;
		//For breaking the rocks
		private float breakTime;
		float rockbreakage = 0.0f;

		// Use this for initialization
		new void Start ()
		{
			base.Start ();
			laserIntialPosition = laser.transform.localPosition;
			//laserMat = laser.GetComponent<Renderer>().material;
			//light = lightGameObject.GetComponent<Light>();
			previousColour = light.color;

		}

		new void Update ()
		{
			base.Update ();
			if (buttonDown == true) {
				//Raycasting to ground
				RaycastHit hit;
				Vector3 forward = transform.TransformVector (Vector3.left);
				//Debug.DrawRay (shootPoint.position, forward, new Color (255, 136, 0), 20, false);
				if (Physics.Raycast (shootPoint.position, forward, out hit, 3)) {
					//Debug.Log ("The ChemCam hit " + hit.transform.name + "At a distance of " + hit.distance);
					ObjectProperties objectProperties = hit.transform.GetComponent<ObjectProperties> ();
					if (objectProperties != null) {
						string rockMaterial = objectProperties.getSimpleMaterial ();
						Debug.Log ("The current material is:" + rockMaterial);

						//Updating the position of the laser and the light
						laser.transform.localPosition = new Vector3 ((-hit.distance) / 2 - 0.5f, 0.068f, 0) + laserIntialPosition;
						laser.transform.localScale = new Vector3 (laser.transform.localScale.x, hit.distance * 70, laser.transform.localScale.z);
						lightGameObject.transform.position = new Vector3 (hit.point.x, hit.point.y, hit.point.z) + transform.right / 9.0f;
						if (lastRock == hit.transform.gameObject) {
							//If we're still on the same rock.
                            
							if (rockbreakage != 0.0f) {
								GetComponent<AudioSource> ().Play ();
								breakTime += Time.deltaTime;
								if (breakTime >= rockbreakage) {
									Debug.Log ("Rock has been broken");
									objectProperties.breakRock ();
								}
							} else {
								Debug.Log ("Rock is unbreakable");
							}
                                
						} else {
							rockbreakage = objectProperties.getSimpleRockBreakage ();
							breakTime = 0;
							if (lastRock == null) {
								lastRock = hit.transform.gameObject;
								//If we haven't hit anything yet.
								laser.SetActive (true);
							}
							switch (rockMaterial) {
							case "nil":
								lightColourToLerp = new Color32 (100, 255, 0, 255);
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
									lightColourToLerp = Color.red;
									break;
								}
							}
						}
					} else
						Debug.Log ("This object has no ObjectProperties script.");
					//lastRock = null;
					lerpColor ();
				} else {
					Debug.Log ("The ChemCam didn't hit anything. Move closer or something isn't working.");
				}
			}
		}

		public override void UseButtonDown ()
		{
			buttonDown = true;
		}

		public override void UseButtonUp ()
		{
			buttonDown = false;
			lastRock = null;
			laser.SetActive (false);
		}

		private void lerpColor ()
		{
			//Debug.Log("TEST");
			Debug.Log (light);
			Debug.Log (lightColourToLerp);
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
				//Test
			}
		}
	}
}

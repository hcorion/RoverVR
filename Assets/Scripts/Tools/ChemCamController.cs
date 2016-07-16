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
        public Light light;
        private Color32 lightColourToLerp;
        private Color32 previousColour;
        private float currentTime;

        // Use this for initialization
        new void Start()
        {
            base.Start();
            laserIntialPosition = laser.transform.localPosition;
            //laserMat = laser.GetComponent<Renderer>().material;
            //light = lightGameObject.GetComponent<Light>();
            previousColour = light.color;

        }

        new void Update()
        {
            base.Update();
            if (buttonDown == true)
            {
                //Raycasting to ground
                RaycastHit hit;
                Vector3 forward = transform.TransformVector(Vector3.right);
                Debug.DrawRay(shootPoint.position, forward, new Color(255, 136, 0), 20, false);
                if (Physics.Raycast(shootPoint.position, forward, out hit, 3))
                {
                    Debug.Log("The ChemCam hit " + hit.transform.name + "At a distance of " + hit.distance);
                    ObjectProperties objectProperties = hit.transform.GetComponent<ObjectProperties>();
                    if (objectProperties != null)
                    {
                        string rockMaterial = objectProperties.getSimpleMaterial();
                        Debug.Log("The current material is:" + rockMaterial);

                        //Updating the position of the laser and the light
                        laser.transform.localPosition = new Vector3(hit.distance / 2 + 0.12f, 0, 0) + laserIntialPosition;
                        laser.transform.localScale = new Vector3(laser.transform.localScale.x, hit.distance * 55, laser.transform.localScale.z);
                        lightGameObject.transform.position = new Vector3(hit.point.x - 0.08f, hit.point.y, hit.point.z);
                        if (lastRock == hit.transform.gameObject)
                        {
                            //If we're still on the same rock.
                        }
                        else
                        {
                            if (lastRock == null)
                            {
                                //If we haven't hit anything yet.
                                laser.SetActive(true);
                            }
                            switch (rockMaterial)
                            {
                                case "nil":
                                    lightColourToLerp = Color.red;
                                    break;
                                case "Aluminium":
                                    lightColourToLerp = new Color32(76, 88, 156, 255);
                                    break;
                                case "Copper":
                                    lightColourToLerp = new Color32(111, 181, 109, 255);
                                    break;
                                default:
                                    {
                                        Debug.LogError("Woops, the material " + rockMaterial + " is not recognized by the ChemCamController Script.");
                                        lightColourToLerp = Color.red;
                                        break;
                                    }
                                    //Instantiate a light here hit.point.position;
                            }
                        }
                    }
                    else
                        Debug.Log("This object has no ObjectProperties script.");
                    //lastRock = null;
                    lerpColor();
                }
                else
                {
                    Debug.Log("The ChemCam didn't hit anything. Move closer or something isn't working.");
                }
            }
        }

        public override void UseButtonDown()
        {
            buttonDown = true;
        }
        public override void UseButtonUp()
        {
            buttonDown = false;
            lastRock = null;
            laser.SetActive(false);
        }
        private void lerpColor()
        {
            //if (lightColourToLerp == null || light.color == null || previousColour == null || currentTime == null)
            //{
            //    return;
            //    Debug.Log("LightColourToLerp == null");
            //}
            Debug.Log("TEST");
            Debug.Log(light);
            Debug.Log(lightColourToLerp);
            const float lerpTime = 2;
            if (currentTime / lerpTime < 1.0) {
                //If we haven't acheived the goal.
            currentTime += Time.deltaTime;
            light.color = Color32.Lerp(previousColour, lightColourToLerp, currentTime / lerpTime);
            laserMat.color = Color32.Lerp(previousColour, lightColourToLerp, currentTime / lerpTime);
            }
            else if (lightColourToLerp != light.color)
            {
                //If we're changing color
                currentTime = 0.0f;
                previousColour = light.color;
                //Test
            }
        }
    }
}

using UnityEngine;
using System.Collections;
namespace NewtonVR
{
    public class ChemCamController : NVRInteractableItem
    {

        public Transform shootPoint;
        private bool buttonDown = false;
		//Is used to tell if the user has changed and is now pointing at a different rock.
		private GameObject lastRock;
		public GameObject laser;
		private Vector3 laserIntialPosition;

        // Use this for initialization
        new void Start()
        {
            base.Start();
			laserIntialPosition = laser.transform.localPosition;
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
                        Debug.Log("The current material is:" + objectProperties.getMaterial());
						if (lastRock == null)
						{
							//If we haven't hit anything yet.
							lastRock = hit.transform.gameObject;
						}
						else if(lastRock == hit.transform.gameObject)
						{
							//If we're still on the same rock.

						}
						else 
						{
							//If we've changed to a new rock (extremely unlikely)
						}
						laser.transform.localPosition = new Vector3(0, hit.distance / 2, 0) + laserIntialPosition;
						laser.transform.localScale = new Vector3(laser.transform.localScale.x, hit.distance / 2, laser.transform.localScale.z );


                    }
                    else
                        Debug.Log("This object has no ObjectProperties script.");
						lastRock = null;
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
    }
}

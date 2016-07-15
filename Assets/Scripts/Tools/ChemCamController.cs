using UnityEngine;
using System.Collections;
namespace NewtonVR
{
	public class ChemCamController : NVRInteractableItem
	{

		public Transform shootPoint;

		bool buttonPressed;

		//Aaron's Power Limit
		public int maxPower = 100;
		public int startingPower = 100;
		//Has a lower limit of 0 and a upper limit of 100
		[Range(0.0f, 100.0f)]
		private float currentPower;
		
				
		// Use this for initialization
		new void Start () 
		{
			base.Start ();
			currentPower = startingPower;
		}
		new void Update () 
		{
			base.Update();
		}

		public override void UseButtonDown ()
		{
			if(currentPower > 0) 
				{
				if(buttonPressed) 
				{
					//To make the battery go down by time, not framerate.
					currentPower -= Time.deltaTime / 2;
					Debug.Log("Current power for ChemCam: " + currentPower);
					//Raycasting to ground
					RaycastHit hit;
					Vector3 forward = transform.TransformVector(Vector3.right);
					Debug.DrawRay(shootPoint.position, forward, Color.red, 20, false);
					if(Physics.Raycast(shootPoint.position, forward, out hit, 3)) {
						Debug.Log("The ChemCam hit " + hit.transform.name + "At a distance of " + hit.distance);
						ObjectProperties objectProperties = hit.transform.GetComponent<ObjectProperties>();
						if(objectProperties != null) {
							Debug.Log("The current material is:" + objectProperties.getMaterial());
						} else
							Debug.Log("This object has no ObjectProperties script.");
					} else {
						Debug.Log("The ChemCam didn't hit anything. Move closer or something isn't working.");
					}
				}	
			}
		}
	}
}

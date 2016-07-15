using UnityEngine;
using System.Collections;
namespace NewtonVR
{
	public class ChemCamController : NVRInteractableItem
	{

		public Transform shootPoint;

				
		// Use this for initialization
		new void Start () 
		{
			base.Start ();
		}
		new void Update () 
		{
			base.Update();
		}

		public override void UseButtonDown ()
		{
			//Raycasting to ground
			RaycastHit hit;
			if(Physics.Raycast(shootPoint.position, Vector3.forward, out hit, 3))
			{
				Debug.Log("The ChemCam hit " + hit.transform.name + "At a distance of " + hit.distance);
				ObjectProperties objectProperties = hit.transform.GetComponent<ObjectProperties> ();
				if( objectProperties != null)
				{
					Debug.Log("The current material is:" + objectProperties.getMaterial());
				}
				else
					Debug.Log("This object has no ObjectProperties script.");
			}
			else
			{
				Debug.Log("The ChemCam didn't hit anything. Move closer or something isn't working.");
			}

		}
	}
}

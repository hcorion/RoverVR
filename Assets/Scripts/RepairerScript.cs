using UnityEngine;
using System.Collections;

public class RepairerScript : MonoBehaviour {
	public GameObject door;
	public FixedJoint objectSnapPoint;

	public GameObject rightController;
	public GameObject leftController;

	public void ToolTriggerEntered(string tool, Collider col)
	{
		Debug.Log("We made it to ToolTriggerEntered. The tool is: " + tool);
		switch (tool)
		 {
            case "SelfieStick":
				Debug.Log("Selfie Stick has been added");
				snapObject(col.gameObject);
				break;
			case "NeutronDetector":
				break;
			case "Binoculars":
				break;
			case "ChemCam":
				Debug.Log("Hit the ChemCam!");
				snapObject(col.gameObject);
				break;
			default:
				Debug.Log("The object " + tool + " is not supported for input.");
				break;
		 }
	}

	public void rfMaterialTriggerEntered(GameObject rfMaterial)
	{
		
	}
	private void snapObject(GameObject obj)
	{
		Debug.Log("We made it to snapObject");
		if(rightController.GetComponent<NewtonVR.NVRHand>() == null)
		{
			Debug.Log("ERROR!");
		}
		//We also should set the object to knematic
		if(rightController.GetComponent<NewtonVR.NVRHand>().CurrentlyInteracting.name != obj.name &&
		 leftController.GetComponent<NewtonVR.NVRHand>().CurrentlyInteracting.name != obj.name)
		{
			objectSnapPoint.connectedBody = obj.GetComponent<Rigidbody>();
		}
		else
		{
			Debug.Log("Looks like it's being held");
		}

	}
}
using UnityEngine;
using System.Collections;

public class RepairerScript : MonoBehaviour {
	public GameObject door;
	public Transform objectSnapPoint;

	public GameObject rightController;
	public GameObject leftController;

	//Used for LerpObjectToSnap

	private GameObject objToLerp;
	private float lerpTime = 2.0f;
	private float currentLerpTime = 0.0f;
	private Transform oldLerpPos = null;
	private bool objIsSnapped = false;
	private GameObject currentTool;
	NewtonVR.NVRHand leftHand;
	NewtonVR.NVRHand rightHand;

	
	void Start()
	{
		if(rightController.GetComponent<NewtonVR.NVRHand>() == null)
		{
			Debug.Log("Error, the right Controller doesn't have NVRHand as a script");
		}
		NewtonVR.NVRHand rightHand = rightController.GetComponent<NewtonVR.NVRHand>();
		rightHand = rightController.GetComponent<NewtonVR.NVRHand>();
		leftHand = leftController.GetComponent<NewtonVR.NVRHand>();
	}
	void Update()
	{
		lerpObjectToSnap();
		if (objIsSnapped == true)
		{
			if (leftHand.CurrentlyInteracting == currentTool || rightHand.CurrentlyInteracting == currentTool)
			{
				currentTool.GetComponent<Rigidbody>().isKinematic = false;
				currentTool = null;
			}
		}
	}
	public void ToolTriggerEntered(string tool, Collider col)
	{
		Debug.Log("We made it to ToolTriggerEntered. The tool is: " + tool);
		if(currentTool != null)
		{
			Debug.Log("We already have a tool that's being used.");
			return;
		}
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
		Debug.Log("the right hand is currently interacting with " + rightHand.CurrentlyInteracting);
		if(leftHand.IsInteracting != obj && rightHand.IsInteracting != obj)
		{
			objToLerp = obj;
			oldLerpPos = objToLerp.transform;
			objIsSnapped = true;
		}
		//We also should set the object to knematic
		//if(rightController.GetComponent<NewtonVR.NVRHand>().CurrentlyInteracting.name != obj.name &&
		// leftController.GetComponent<NewtonVR.NVRHand>().CurrentlyInteracting.name != obj.name)
		//{
		
		//}
		//else
		//{
		//	Debug.Log("Looks like it's being held");
		//

	}
    private void lerpObjectToSnap()
    {
		if(objToLerp == null)
		{
			//If we haven't placed an object on the tble
		}
		else if (oldLerpPos == null || oldLerpPos.name != objToLerp.name)
		{
			oldLerpPos = objToLerp.transform;
		}
        else if (currentLerpTime / lerpTime < 1.0)
        {
			//If we haven't reached our goal.
			currentLerpTime += Time.deltaTime;
			Debug.Log("It is" + objToLerp);
            objToLerp.transform.position = Vector3.Lerp(oldLerpPos.position, objectSnapPoint.position, currentLerpTime / lerpTime);
        }
		else
		{
			objToLerp.GetComponent<Rigidbody>().isKinematic = true;
		}
    }
}
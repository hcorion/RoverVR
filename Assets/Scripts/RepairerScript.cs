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
	
	void Start()
	{

	}
	void Update()
	{
		lerpObjectToSnap();
	}
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
		//objectSnapPoint.connectedBody = obj.GetComponent<Rigidbody>();
		objToLerp = obj;
		oldLerpPos = objToLerp.transform;
		if(rightController.GetComponent<NewtonVR.NVRHand>() == null)
		{
			Debug.Log("ERROR!");
		}
		else
		{
			Debug.Log("The NVRHand is: " + rightController.GetComponent<NewtonVR.NVRHand>());
		}
		Debug.Log("Made it to this place, here!");
		NewtonVR.NVRHand hand;
		hand = rightController.GetComponent<NewtonVR.NVRHand>();
		Debug.Log("the hand is currently interacting with " + hand.CurrentlyInteracting);
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
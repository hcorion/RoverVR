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
	//Used for basic detection of what the hand is holding.
	private GameObject currentTool;
	public NewtonVR.NVRHand leftHand;
	public NewtonVR.NVRHand rightHand;
	//Used for the detection of ingots.
	private GameObject[] ingots;
	private int toolIndex;
	void Start()
	{
		if(rightController.GetComponent<NewtonVR.NVRHand>() == null)
		{
			Debug.Log("Error, the right Controller doesn't have NVRHand as a script");
		}
		//NewtonVR.NVRHand rightHand = rightController.GetComponent<NewtonVR.NVRHand>();
		//rightHand = rightController.GetComponent<NewtonVR.NVRHand>();
		//leftHand = leftController.GetComponent<NewtonVR.NVRHand>();
	}
	void Update()
	{
		lerpObjectToSnap();
		if (objIsSnapped == true)
		{
			door.SetActive(false);
			Debug.Log("The left hand is currently interacting with: " + leftHand.CurrentlyInteracting);
			Debug.Log("The right hand is currently interacting with: " + rightHand.CurrentlyInteracting);
			if (leftHand.CurrentlyInteracting == currentTool || rightHand.CurrentlyInteracting == currentTool)
			{
				//If we're picking up the tool again.
				if (currentTool.GetComponent<Rigidbody>() == null)
				{
					Debug.Log("Hmm, this tool doesn't have a rigidbody.");
				}
				currentTool.GetComponent<Rigidbody>().isKinematic = false;
				currentTool = null;
				toolIndex = 23;
				objIsSnapped = false;
			}
		}
		else
		{
			door.SetActive(true);
			////You need to add the removal of the elements from inside the 'furnace'
		}
	}
	public void ToolTriggerEntered(string tool, GameObject toolObj)
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
				toolIndex = 0;
				snapObject(toolObj);
				break;
			case "NeutronDetector":
				toolIndex = 1;
				break;
			case "Binoculars":
				toolIndex = 2;
				break;
			case "ChemCam":
				toolIndex = 3;
				Debug.Log("Hit the ChemCam!");
				snapObject(toolObj);
				break;
			default:
				Debug.Log("The object " + tool + " is not supported for input.");
				break;
		 }
	}

	public void IngotTriggerEntered(GameObject rfMaterial)
	{
		bool isAlreadyAdded = false;
		for(int i = 0; i >= ingots.Length; i++)
		{
			if (rfMaterial == ingots[i])
			{
				return;
			}
		}
		if(ingots.Length != 0)
		{
			ingots[ingots.Length+1] = rfMaterial;
		}
		else
		{
			ingots[0] = rfMaterial;
		}
	}
	public void repairTool()
	{
		/*Selfie Stick takes: 
		2 100% Aluminum
		1 100% Copper*/
		/*ChemCam takes
		1 50% Aluminum
		1 20% Copper
		*/
		int AluminumRepairValue = 0;
		int CopperRepairValue = 0;
		switch (toolIndex)
		{
            case 0:
				Debug.Log("The Selfie Stick can't yet be repaired");
				break;
			case 1:
				Debug.Log("The Selfie Stick can't yet be repaired");
				break;
			case 3:
				Debug.Log("I'm going to repair the ChemCam.");
				AluminumRepairValue = 50;
				CopperRepairValue = 20;
				break;
			case 23:
				Debug.Log("No tool is present, or it has been removed.");
				break;
			default:
				Debug.Log("That tool isn't yet added to the RepairerScript");
				break;
		}
		int currentAluminium = 0;
		int currentCopper = 0;
		foreach(GameObject ingot in ingots)
		{
			string name = ingot.GetComponent<IngotProperties>().GetName();
			if(name == "Aluminum")
			{
				currentAluminium = Mathf.FloorToInt(ingot.transform.localScale.x)*100;
			}
			else if (name == "Copper")
			{
				currentCopper = Mathf.FloorToInt(ingot.transform.localScale.x)*100;
			}
			else
			{
				Debug.Log("Woops, RepairerScript doesn't recognize the material of type: " + name);
			}
		}
		if(currentAluminium >= AluminumRepairValue)
		{

		}
	}
	private void snapObject(GameObject obj)
	{
		Debug.Log("The gameobject of snapObject is: " + obj + " and is called " + obj.name);
		Debug.Log("the right hand is currently interacting with " + rightHand.CurrentlyInteracting);
		if(leftHand.IsInteracting != obj || rightHand.IsInteracting != obj)
		{
			objToLerp = obj;
			oldLerpPos = objToLerp.transform;
			currentTool = obj;
			objIsSnapped = true;
			objToLerp.GetComponent<Rigidbody>().isKinematic = true;
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
        else if (currentLerpTime / lerpTime < 0.1)
        {
			//If we haven't reached our goal.
			currentLerpTime += Time.deltaTime;
			Debug.Log("It is" + objToLerp);
            objToLerp.transform.position = Vector3.Lerp(oldLerpPos.position, objectSnapPoint.position, currentLerpTime / lerpTime);
        }
		else
		{
			//If we have reached our goal and are waiting for new instructions.
		}
    }
}
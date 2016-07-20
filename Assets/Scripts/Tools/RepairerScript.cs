using UnityEngine;
using System.Collections;

public class RepairerScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToolTriggerEntered(string tool, Collider col)
	{
		switch (tool)
		 {
            case "SelfieStick":
				Debug.Log("Selfie Stick has been added");
				snapObject();
				break;
			case "NeutronDetector":
				break;
			case "Binoculars":
				break;
			case "ChemCam":
				break;
			default:
				Debug.Log("The object " + tool + " is not supported for input.");
				break;
		 }
	}

	public void rfMaterialTriggerEntered(GameObject rfMaterial)
	{
		
	}
	private void snapObject()
	{

	}
}
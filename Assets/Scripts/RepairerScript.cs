﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RepairerScript : MonoBehaviour
{
	public GameObject door;
	public Transform objectSnapPoint;

	public GameObject rightController;
	public GameObject leftController;

	public GameObject objectDropPoint;
	//Used for the Repair Button

	public NewtonVR.NVRButton button;

	private bool ButtonHasPushed = false;

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

	private bool checkIfAdded = false;

	private GameObject objStillInHand = null;
	//Used for the detection of ingots.
	//private GameObject[] ingots;
	List<GameObject> ingots = new List<GameObject> ();
	private int toolIndex;

	void Start ()
	{
		if (rightController.GetComponent<NewtonVR.NVRHand> () == null) {
			//Debug.Log ("Error, the right Controller doesn't have NVRHand as a script");
		}
		//NewtonVR.NVRHand rightHand = rightController.GetComponent<NewtonVR.NVRHand>();
		//rightHand = rightController.GetComponent<NewtonVR.NVRHand>();
		//leftHand = leftController.GetComponent<NewtonVR.NVRHand>();
	}

	void Update ()
	{
		if (button.ButtonIsPushed && ButtonHasPushed == false) {
			repairTool ();
			ButtonHasPushed = true;
		}
		lerpObjectToSnap ();
		if (objIsSnapped == true) {
			door.SetActive (false);
			//Debug.Log ("The left hand is currently interacting with: " + leftHand.CurrentlyInteracting);
			//Debug.Log ("The right hand is currently interacting with: " + rightHand.CurrentlyInteracting);
			if (leftHand.CurrentlyInteracting == currentTool || rightHand.CurrentlyInteracting == currentTool) {
				//Debug.Log ("We've removed the tool.");
				//If we're picking up the tool again.
				if (currentTool.GetComponent<Rigidbody> () == null) {
					//Debug.Log ("Hmm, this tool doesn't have a rigidbody.");
				}
				currentTool.GetComponent<Rigidbody> ().isKinematic = false;
				currentTool = null;
				toolIndex = 23;
				objIsSnapped = false;
				objToLerp = null;
			}
		} else {
			//Debug.Log ("Object is not snapped.");
			door.SetActive (true);
			//currentTool.GetComponent<Rigidbody>().isKinematic = true;
			////You need to add the removal of the elements from inside the 'furnace'
		}
		if (checkIfAdded) {
			if (rightHand.IsInteracting != objStillInHand && leftHand.IsInteracting != objStillInHand) {
				//Debug.Log ("The user has let go of the object.");
				checkIfAdded = false;
				objToLerp = objStillInHand;
				//oldLerpPos = objToLerp.transform;
				currentTool = objStillInHand;
				objIsSnapped = true;
				currentTool.GetComponent<Rigidbody> ().isKinematic = true;
			}
		}
	}

	public void ToolTriggerEntered (string tool, GameObject toolObj)
	{
		//Debug.Log ("We made it to ToolTriggerEntered. The tool is: " + tool);
		if (currentTool != null) {
			//Debug.Log ("We already have a tool, " + currentTool + " that's being used.");
			return;
		}
		switch (tool) {
		case "SelfieStick":
			//Debug.Log ("Selfie Stick has been added");
			toolIndex = 0;
			snapObject (toolObj);
			break;
		case "NeutronDetector":
			toolIndex = 1;
			snapObject (toolObj);
			break;
		case "Binoculars":
			toolIndex = 2;
			break;
		case "ChemCam":
			toolIndex = 3;
			//Debug.Log ("The ChemCam has been added.");
			snapObject (toolObj);
			break;
		default:
			//Debug.Log ("The object " + tool + " is not supported for input.");
			break;
		}
	}

	public void IngotTriggerEntered (GameObject rfMaterial)
	{
		bool isAlreadyAdded = false;
		if ((ingots != null) && (ingots.Any ())) {
			ingots.Add (rfMaterial);
		} else {
			foreach (GameObject ingot in ingots) {
				if (ingot == rfMaterial) {
					isAlreadyAdded = true;
				}
			}
			if (isAlreadyAdded != true) {
				ingots.Add (rfMaterial);
			}
		}
	}

	public void repairTool ()
	{
		/*Selfie Stick takes: 
		2 100% Aluminum
		1 100% Copper*/
		/*ChemCam takes
		1 50% Aluminum
		1 20% Copper
		*/
		if (currentTool == null) {
			//Debug.Log ("CurrentTool is equal to null in repairTool");
			return;
		}
		int AluminumRepairValue = 0;
		int CopperRepairValue = 0;
		switch (toolIndex) {
		case 0:
			//Debug.Log ("The Selfie Stick can't yet be repaired");
			AluminumRepairValue = 50;
			CopperRepairValue = 20;
			break;
		case 1:
			//Debug.Log ("The Selfie Stick can't yet be repaired");
			AluminumRepairValue = 50;
			CopperRepairValue = 20;
			break;
		case 3:
			//Debug.Log ("I'm going to repair the ChemCam.");
			AluminumRepairValue = 50;
			CopperRepairValue = 20;
			break;
		case 23:
			//Debug.Log ("No tool is present, or it has been removed.");
			AluminumRepairValue = 50;
			CopperRepairValue = 20;
			break;
		default:
			//Debug.Log ("That tool isn't yet added to the RepairerScript");
			AluminumRepairValue = 50;
			CopperRepairValue = 20;
			break;
		}
		int currentAluminium = 0;
		int currentCopper = 0;
		foreach (GameObject ingot in ingots) {
			IngotProperties ingotProperties = ingot.GetComponent<IngotProperties> ();
			string name = ingotProperties.GetName ();
			if (name == "Aluminum") {
				currentAluminium = Mathf.RoundToInt (ingotProperties.GetValue () * 100);
			} else if (name == "Copper") {
				currentCopper = Mathf.RoundToInt (ingotProperties.GetValue () * 100);
			} else {
				//Debug.Log ("Woops, RepairerScript doesn't recognize the material of type: " + name);
			}
		}
		if (currentAluminium >= AluminumRepairValue) {
			//Debug.Log ("You have enough Aluminum");
			if (currentCopper >= CopperRepairValue) {
				//Debug.Log ("You have enough copper");
				currentTool.transform.position = objectDropPoint.transform.position;
				//int i = ingots.Count;
				foreach (GameObject ingot in ingots) {
					Object.Destroy (ingot);
					//ingots.RemoveAt(i);
					//i--;
				}
				currentTool.GetComponent<ToolProperties> ().Repair ();
				//Clean up stuff for next tool repair.
				toolRemoved (currentTool);
				objIsSnapped = false;
				currentTool.GetComponent<Rigidbody> ().isKinematic = false;
				currentTool = null;
				ButtonHasPushed = false;
				objToLerp = null;
			} else {
				//Debug.Log ("You do not have enough copper.");
				ButtonHasPushed = false;
			}
		} else {
			//Debug.Log ("You do not have enough aluminum. You have " + currentAluminium + " and need " + AluminumRepairValue);
			ButtonHasPushed = false;
		}
	}

	private void snapObject (GameObject obj)
	{
		//Debug.Log ("The gameobject of snapObject is: " + obj + " and is called " + obj.name);
		//Debug.Log ("the right hand is currently interacting with " + rightHand.CurrentlyInteracting);
		if (leftHand.IsInteracting != obj && rightHand.IsInteracting != obj) {
			objToLerp = obj;
			//oldLerpPos = objToLerp.transform;
			currentTool = obj;
			objIsSnapped = true;
			currentTool.GetComponent<Rigidbody> ().isKinematic = true;
		} else {
			//Possibly enter into the Update() function to continue checking if the object is being added.
			//Debug.Log ("The object " + obj.name + " is still in the users hand.");
			checkIfAdded = true;
			objStillInHand = obj;
		}

	}

	private void lerpObjectToSnap ()
	{
		if (objToLerp == null) {
			//If we haven't placed an object on the tble
		} else if (oldLerpPos == null || oldLerpPos.name != objToLerp.name) {
			//If we haven't started the lerp yet.
			oldLerpPos = objToLerp.transform;
		} else if (currentLerpTime / lerpTime < 1) {
			//Debug.Log ("Currently Lerping");
			//If we haven't reached our goal.
			currentLerpTime += Time.deltaTime;
			//Debug.Log ("It is " + objToLerp);
			objToLerp.transform.position = Vector3.Lerp (oldLerpPos.position, objectSnapPoint.position, currentLerpTime / lerpTime);
		} else {
			//If we have reached our goal and are waiting for new instructions.
		}
	}

	public void toolRemoved (GameObject tool)
	{
		/*if (checkIfAdded) {
			Debug.Log ("The user has now removed " + tool + "which never added.");
		}
		if (currentTool != null) { //&& objToLerp != null && objIsSnapped != false)
			if (tool == currentTool) {
				currentTool = null;
				objToLerp = null;
				objIsSnapped = false;
			} else {
				Debug.Log ("We've already got a new tool, " + currentTool.name + " as the currentTool.");
			}

		} else {
			Debug.Log ("We've already recorded the tool as removed.");
		}*/
	}
}
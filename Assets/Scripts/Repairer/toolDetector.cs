﻿using UnityEngine;
using System.Collections;
public class toolDetector : MonoBehaviour {
	public RepairerScript repairerScript;
	public bool isToolDetector;
	void OnTriggerEnter(Collider other)
     {
		 Debug.Log("We made it here!");
		 if(other.gameObject.GetComponent<ToolProperties>() != null)// && isToolDetector == true)
		 {
			 Debug.Log("The object " + other.gameObject.name + " is a tool");
			 Debug.Log("I've hit a Tool!");
		 	repairerScript.ToolTriggerEntered(other.gameObject.GetComponent<ToolProperties>().getTool(), other);
		 }
		 else if (other.gameObject.tag == "" && isToolDetector == false)
		 {
			 Debug.Log("I've hit a rfMaterial!");
			 repairerScript.rfMaterialTriggerEntered(other.gameObject);
		 }
		 else
		 {
			 Debug.Log("This object doesn't have toolProperties or isn't an ingot");
			 Debug.Log("The object " + other.gameObject.name + " doesn't work.");
		 }
     }
}
using UnityEngine;
using System.Collections;
public class toolDetector : MonoBehaviour {
	public RepairerScript repairerScript;
	public bool isToolDetector;
	void OnTriggerEnter(Collider other)
     {
		 GameObject otherRB = other.attachedRigidbody.gameObject;
		 Debug.Log("We made it here!");
		 if(otherRB.GetComponent<ToolProperties>() != null)// && isToolDetector == true)
		 {
			 Debug.Log("The object " + otherRB.name + " is a tool");
			 Debug.Log("I've hit a Tool!");
		 	repairerScript.ToolTriggerEntered(otherRB.GetComponent<ToolProperties>().getTool(), other);
		 }
		 else if (otherRB.tag == "" && isToolDetector == false)
		 {
			 Debug.Log("I've hit a rfMaterial!");
			 repairerScript.rfMaterialTriggerEntered(other.gameObject);
		 }
		 else
		 {
			 Debug.Log("This object doesn't have toolProperties or isn't an ingot");
			 Debug.Log("The object " + otherRB.name + " doesn't work.");
		 }
     }
}
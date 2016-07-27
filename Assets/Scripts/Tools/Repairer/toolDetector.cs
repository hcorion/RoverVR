using UnityEngine;
using System.Collections;
public class toolDetector : MonoBehaviour {
	public RepairerScript repairerScript;
	public bool isToolDetector;
	void OnTriggerEnter(Collider other)
     {
		 GameObject parent = other.transform.root.gameObject;
		 Debug.Log("We made it here!");
		 if(parent.GetComponent<ToolProperties>() != null)// && isToolDetector == true)
		 {
			 Debug.Log("The object " + parent.name + " is a tool");
			 Debug.Log("I've hit a Tool!");
		 	repairerScript.ToolTriggerEntered(parent.GetComponent<ToolProperties>().getTool(), parent);
		 }
		 else if (other.GetComponent<IngotProperties>() != null && isToolDetector == false)
		 {
			 Debug.Log("I've hit a rfMaterial!");
			 repairerScript.IngotTriggerEntered(other.gameObject);
		 }
		 else
		 {
			 Debug.Log("The object " + parent.name + " isn't a tool or an ingot.");
		 }
     }
	 void OnTriggerExit(Collider other)
	 {
		 GameObject parent = other.transform.root.gameObject;
		 if(isToolDetector == true && other.transform.parent.GetComponent<ToolProperties>() != null)
		 {
			 //tool has exited.
			 repairerScript.toolRemoved(other.transform.parent.gameObject);
			 Debug.Log(other.transform.parent.name + " has been removed from the toolDetector.");
		 }
	 }
}
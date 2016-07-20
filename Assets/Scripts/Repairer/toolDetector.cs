using UnityEngine;
using System.Collections;
public class toolDetector : MonoBehaviour {
	public RepairerScript repairerScript;
	public bool isToolDetector;
	void OnTriggerEnter(Collider other)
     {
		 Debug.Log("We made it here!");
		 if(other.gameObject.GetComponent<ToolProperties>() != null && isToolDetector == true)
		 {
			 Debug.Log("I've hit a Tool!");
		 	repairerScript.ToolTriggerEntered(other.gameObject.GetComponent<ToolProperties>().getTool(), other);
		 }
		 else if (other.gameObject.tag == "" && isToolDetector == false)
		 {
			 Debug.Log("I've hit  a rfMaterial!");
			 repairerScript.rfMaterialTriggerEntered(other.gameObject);
		 }
     }
}
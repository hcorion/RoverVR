using UnityEngine;
using System.Collections;
public class toolDetector : MonoBehaviour {
	public RepairerScript repairerScript;
	public bool isToolDetector;
	void OnTriggerEnter(Collider other)
     {
		 if(other.gameObject.GetComponent<ToolProperties>() != null && isToolDetector == true)
		 {
		 	repairerScript.ToolTriggerEntered(other.gameObject.GetComponent<ToolProperties>().getTool(), other);
		 }
		 else if (other.gameObject.tag == "" && isToolDetector == false)
		 {
			 repairerScript.rfMaterialTriggerEntered(other.gameObject);
		 }
     }
}
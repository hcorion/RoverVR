using UnityEngine;
using System.Collections;
public class toolDetector : MonoBehaviour {
	public RepairerScript repairerScript;
	public bool isToolDetector;
	void OnTriggerEnter(Collider other)
     {
		 if(other.gameObject.GetComponent<toolProperties>() != null && isToolDetector == true)
		 {
		 	repairerScript.ToolTriggerEntered(other.gameObject.GetComponent<toolProperties>().getTool(), other);
		 }
		 else if (other.gameObject.tag == "" && isToolDetector == false)
		 {
			 repairerScript.rfMaterialTriggerEntered(other.gameObject);
		 }
     }
}
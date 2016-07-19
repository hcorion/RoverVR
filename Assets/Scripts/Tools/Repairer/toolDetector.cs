using UnityEngine;
using System.Collections;

public class toolDetector : MonoBehaviour {
	void OnTriggerEnter(Collider other)
     {
         switch (other.gameObject.GetComponent<toolProperties>().getTool())
		 {
            case "SelfieStick":
				break;
			default:
				Debug.Log("That object is not supported for input.");
				break;
		 }
     }
}

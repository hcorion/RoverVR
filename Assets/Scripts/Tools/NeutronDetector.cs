using UnityEngine;
using System.Collections;

public class NeutronDetector : MonoBehaviour
{
	public float scanDistance;

	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			print (GetMoistureContent ());
		}
	}

	string GetMoistureContent ()
	{
		RaycastHit hit;
		Vector3 forward = transform.TransformVector (Vector3.forward);
		Ray ray = new Ray (transform.position, forward);
		if (Physics.Raycast (ray, out hit, scanDistance)) {
			ObjectProperties objProp = hit.collider.GetComponent<ObjectProperties> ();
			if (objProp != null) {
				return objProp.moistureContent.ToString ();
			} else {
				return ("No Moisture Found");
			}
		} else {
			return ("No Moisture Found");
		}
	}
}

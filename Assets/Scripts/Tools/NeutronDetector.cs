using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NeutronDetector : MonoBehaviour
{
	public float scanDistance;
	public string units;
	public GameObject textObject;

	Text content;

	void Start ()
	{
		content = textObject.GetComponent<Text> ();
		content.text = "";
	}

	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			content.text = GetMoistureContent ();
		} else {
			content.text = "";
		}
	}

	string GetMoistureContent ()
	{
		RaycastHit hit;
		Vector3 forward = transform.TransformVector (Vector3.forward);
		Ray ray = new Ray (transform.position, forward);
		if (Physics.Raycast (ray, out hit, scanDistance)) {
			WaterSource waterSrc = hit.collider.GetComponent<WaterSource> ();
			if (waterSrc != null) {
				float moisture = waterSrc.moistureContent / Vector3.Distance (hit.point, hit.transform.position);
				Debug.DrawLine (hit.transform.position, hit.point, Color.red, 20, false);
				if (moisture > 0) {
					return moisture.ToString () + units;
				} else {
					return ("None");
				}
			} else {
				return ("None");
			}
		} else {
			return ("None");
		}
	}
}

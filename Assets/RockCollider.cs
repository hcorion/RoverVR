using UnityEngine;
using System.Collections;

public class RockCollider : MonoBehaviour
{
	public string element = "";

	void OnTriggerEnter (Collider c)
	{
		ObjectProperties objProp = c.gameObject.GetComponent<ObjectProperties> ();

		if (objProp != null) {
			element = objProp.getMaterial ();
			Destroy (c.gameObject);
		}
	}
}

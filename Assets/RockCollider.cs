using UnityEngine;
using System.Collections;

public class RockCollider : MonoBehaviour
{
	public string element = "";
	public static bool activated = false;

	void OnTriggerEnter (Collider c)
	{
		ObjectProperties objProp = c.gameObject.GetComponent<ObjectProperties> ();

		if (objProp != null) {
			element = objProp.getMaterial ();
			activated = true;
			Destroy (c.gameObject);
		}
	}
}

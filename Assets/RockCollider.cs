using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RockCollider : MonoBehaviour
{
	public string element = "";

	public GameObject[] refinedElements;
	public string[] refinedElementNames;

	private Dictionary<string, GameObject> elements = new Dictionary<string, GameObject> ();

	void Start ()
	{
		for (int i = 0; i < refinedElements.Length; ++i) {
			elements.Add (refinedElementNames [i], refinedElements [i]);
		}
	}

	void OnTriggerEnter (Collider c)
	{
		ObjectProperties objProp = c.gameObject.GetComponent<ObjectProperties> ();

		if (objProp != null) {
			element = objProp.getMaterial ();
			
			Instantiate (GetElement (element));
			Destroy (c.gameObject);
		}
	}

	GameObject GetElement (string element)
	{
		GameObject value;
		elements.TryGetValue (element, out value);
		return value;
	}
}

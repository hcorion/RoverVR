using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RockCollider : MonoBehaviour
{
	public string element = "";

	public float spawnOffsetX;
	public float spawnOffsetY;
	public float spawnOffsetZ;

	public Transform body;

	public GameObject[] refinedElements;
	public string[] refinedElementNames;

	private Dictionary<string, GameObject> elements = new Dictionary<string, GameObject> ();
	private Dictionary<string, float> complexMaterials;

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
			element = objProp.getSimpleMaterial ();
			
			Instantiate (GetElement (element), new Vector3 (body.position.x + spawnOffsetX, body.position.y + spawnOffsetY, body.position.z + spawnOffsetZ), Quaternion.identity);
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

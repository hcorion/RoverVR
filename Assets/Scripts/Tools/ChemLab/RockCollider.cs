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
	private Dictionary<string, float> materialDictionary;

	private ArrayList materialNames;

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
			materialDictionary = objProp.getMaterialDictionary ();
			materialNames = objProp.getMaterialNames ();

			for (int i = 0; i < materialNames.Count; ++i) {
				float value;
				materialDictionary.TryGetValue (materialNames [i].ToString (), out value);
				element = materialNames [i].ToString () + " " + value + "% \n";

				/*GameObject spawned = (GameObject)Instantiate (GetElement (materialNames [i].ToString ()), new Vector3 (body.position.x + spawnOffsetX, body.position.y + spawnOffsetY, body.position.z + spawnOffsetZ), Quaternion.identity);
				Vector3 scale = spawned.transform.localScale;
				spawned.transform.localScale = new Vector3 (scale.x / (100 / value), scale.y / (100 / value), scale.z / (100 / value));*/

				Instantiate (GetElement (materialNames [i].ToString (), value), new Vector3 (body.position.x + spawnOffsetX, body.position.y + spawnOffsetY, body.position.z + spawnOffsetZ), Quaternion.identity);
			}

			Destroy (c.gameObject);
		}
	}

	GameObject GetElement (string name, float percent)
	{
		GameObject value;
		elements.TryGetValue (name, out value);

		Vector3 scale = value.transform.localScale;
		value.transform.localScale = new Vector3 (scale.x / (100 / percent), scale.y / (100 / percent), scale.z / (100 / percent));

		return value;
	}
}

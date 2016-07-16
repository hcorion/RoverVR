using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class ChemLab : MonoBehaviour
{
	public GameObject activationPlate;
	public GameObject textObject;

	public GameObject[] refinedElements;
	public string[] refinedElementNames;

	private Dictionary<string, GameObject> elements = new Dictionary<string, GameObject> ();

	Text element;
	bool activated;

	int timesInstantiated;

	void Start ()
	{
		for (int i = 0; i < refinedElements.Length; ++i) {
			elements.Add (refinedElementNames [i], refinedElements [i]);
		}

		activated = textObject.GetComponent<RockCollider> ().activated;
		element = textObject.GetComponent<Text> ();

		timesInstantiated = 0;
	}

	void Update ()
	{
		element.text = activationPlate.GetComponent<RockCollider> ().element;
		print ("UI Name of Element: " + element.text);

		if (activated == true) {
			print ("Amount of Times Instantiated: " + timesInstantiated);
			print ("Activated State Before Create: " + activated);

			Instantiate (GetElement (element.text));
			activated = false;

			print ("Activated State After Create: " + activated);
			print ("Final Element Name: " + element.text);

			timesInstantiated++;
		}
	}

	GameObject GetElement (string element)
	{
		GameObject value;
		elements.TryGetValue (element, out value);
		return value;
	}
}

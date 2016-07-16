using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChemLabUI : MonoBehaviour
{
	public GameObject activationPlate;
	public GameObject textObject;

	Text element;

	void Start ()
	{
		element = textObject.GetComponent<Text> ();
	}

	void Update ()
	{
		element.text = activationPlate.GetComponent<RockCollider> ().element;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChemLab : MonoBehaviour
{
	public GameObject activationPlate;
	public GameObject textObject;

	Text element;

	void Start ()
	{
		element = textObject.GetComponent<Text> ();
		element.text = activationPlate.GetComponent<RockCollider> ().element;
	}

	void Update ()
	{
		element.text = activationPlate.GetComponent<RockCollider> ().element;
		Instantiate (GameObject.Find (element.text));
	}
}

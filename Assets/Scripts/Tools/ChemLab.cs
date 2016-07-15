using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChemLab : MonoBehaviour
{
	public GameObject activationPlate;
	public GameObject textObject;

	Text elements;

	void Start ()
	{
		elements = textObject.GetComponent <Text> ();
		elements.text = "";
	}

	void OnCollisionEnter (Collider c)
	{
		
	}
}

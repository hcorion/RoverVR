using UnityEngine;
using System.Collections;

public class IngotProperties : MonoBehaviour
{
	//Name and Value assigned by rock collider script

	private string ingotName;
	private float ingotValue;

	public void SetName (string name)
	{
		ingotName = name;
	}

	public void SetValue (float value)
	{
		ingotValue = value;
	}

	public string GetName ()
	{
		return ingotName;
	}

	public float GetValue ()
	{
		return ingotValue;
	}
}

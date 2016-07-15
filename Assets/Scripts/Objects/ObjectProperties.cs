using UnityEngine;
using System.Collections;

public class ObjectProperties : MonoBehaviour
{
	//public readonly string[] Materials = {"", ""};
	public enum materials { Aluminium, Copper };
	public materials materialSelect;

	void Start ()
	{
	}

	void Update ()
	{

	}
	public string getMaterial()
	{
		return materialSelect.ToString();
	}
}

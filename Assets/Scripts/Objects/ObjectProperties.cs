using UnityEngine;
using System.Collections;

public class ObjectProperties : MonoBehaviour
{
	//public readonly string[] Materials = {"", ""};
	public enum materials
	{
		nil,
		Aluminum,
		Copper}

	;

	public materials materialSelect;

	public string getMaterial ()
	{
		return materialSelect.ToString ();
	}
}

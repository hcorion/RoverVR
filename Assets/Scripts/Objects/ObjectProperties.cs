using UnityEngine;
using System.Collections;

public class ObjectProperties : MonoBehaviour
{
	//public readonly string[] Materials = {"", ""};
	/*If your changing the following values it also needs to be changed in the scripts listed below:
	- ChemCamController
	*/

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

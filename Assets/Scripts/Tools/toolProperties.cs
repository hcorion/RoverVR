using UnityEngine;
using System.Collections;

public class toolProperties : MonoBehaviour {
	 public enum Tools{SelfieStick, NeutronDetector, Binoculars, ChemCam};
     
     //This is what you need to show in the inspector.
     public Tools tool;

	public string getTool()
	{
		return tool.ToString();
	}
}

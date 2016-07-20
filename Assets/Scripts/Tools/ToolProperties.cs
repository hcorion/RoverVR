using UnityEngine;
using System.Collections;

public class ToolProperties : MonoBehaviour {
	public enum toolList{SelfieStick, NeutronDetector, Binoculars, ChemCam};
     
     //This is what you need to show in the inspector.
     public toolList tool;

	public string getTool()
	{
		return tool.ToString();
	}
}

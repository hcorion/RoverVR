using UnityEngine;
using System.Collections;

public class ToolProperties : MonoBehaviour {
	public enum toolList{SelfieStick, NeutronDetector, Binoculars, ChemCam};
     
     //This is what you need to show in the inspector.
     public toolList tool;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string getTool()
	{
		return tool.ToString();
	}
}

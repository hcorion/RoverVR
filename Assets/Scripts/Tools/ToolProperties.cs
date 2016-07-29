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
    public void Repair()
    {
        switch (getTool())
        {
            case "SelfieStick":
                Debug.Log("Selfie Stick has been repaired");
				gameObject.GetComponent<NewtonVR.SelfieStickController>().dmgUI.health = gameObject.GetComponent<NewtonVR.SelfieStickController>().dmgUI.maxHealth;
                break;
            case "NeutronDetector":
				gameObject.GetComponent<NewtonVR.NeutronDetector>().dmgUI.health = gameObject.GetComponent<NewtonVR.NeutronDetector>().dmgUI.maxHealth;
                break;
            case "Binoculars":
				Debug.Log("Wait, what? You can't repair the binoculars.");
                break;
            case "ChemCam":
				gameObject.GetComponent<NewtonVR.ChemCamController>().dmgUI.health = gameObject.GetComponent<NewtonVR.ChemCamController>().dmgUI.maxHealth;
                break;
            default:
                Debug.Log("The object " + tool + " is not supported for input.");
                break;
        }
    }
}
using UnityEngine;
using System.Collections;
using Valve.VR;

//This script needs to be attached to a controller
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveInputTest : MonoBehaviour {
	
	//This obtains a script from the controller
	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;


	void Awake ()
	{
		device = SteamVR_Controller.Input((int)trackedObj.index);
	}
	
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(getTriggerDown())
		{
			Debug.Log("Trigger is down!");
		}
		//Look at device.GetTouchUp as an alternative
	}

	bool getTriggerDown()
	{
		return device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
	}
}

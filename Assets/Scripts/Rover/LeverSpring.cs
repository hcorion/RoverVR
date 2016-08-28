using UnityEngine;
using System.Collections;

public class LeverSpring : MonoBehaviour {
    private HingeJoint hinge;
    private NewtonVR.NVRLever lever;


    void Awake ()
    {
        hinge = gameObject.GetComponent<HingeJoint>();
        lever = gameObject.GetComponent<NewtonVR.NVRLever>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (lever.IsAttached && hinge.useSpring == true)
        {
            hinge.useSpring = false;
        }
        else if (lever.AttachedHand == null && hinge.useSpring == false)
        {
            hinge.useSpring = true;
        }

    }
}

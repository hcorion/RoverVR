using UnityEngine;
using System.Collections;

public class testWheelRotation : MonoBehaviour {
	private Rigidbody wheel;
	private Quaternion startRotation;
	private ConfigurableJoint joint;
	public GameObject sphere;
	// Use this for initialization
	void Start () {
		wheel = gameObject.GetComponent<Rigidbody>();
		startRotation = transform.rotation;
		joint = gameObject.GetComponent<ConfigurableJoint> ();
		//joint.SetTargetRotationLocal (Quaternion.Euler (0, 0, 270), startRotation);
	}
	
	// Update is called once per frame
	void Update () {
		wheel.AddRelativeTorque(0, 0, .2f, ForceMode.Force);
		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, sphere.transform.eulerAngles.y, transform.eulerAngles.z);
		//joint.SetTargetRotationLocal (Quaternion.Euler (0, 0, 0), startRotation);
		//joint.SetTargetRotationLocal (Quaternion.Euler (0, 0, 270), startRotation);
		//joint.SetTargetRotation(sphere.transform.rotation, startRotation);
	}

	
}

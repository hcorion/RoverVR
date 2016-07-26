using UnityEngine;
using System.Collections;

public class Skateboard : MonoBehaviour
{
	public float speed = 0;
	public float maxspeed;
	public float rotationspeed;
	public GameObject axle;
	public GameObject low6;
	public HingeJoint hinge;
	public HingeJoint wheelhinge;

	void Start ()
	{
		hinge = axle.GetComponent<HingeJoint> ();
		wheelhinge = low6.GetComponent<HingeJoint> ();
	}

	float getRotation ()
	{
		return low6.transform.localRotation.eulerAngles.z;
	}

	float getAngle ()
	{
		
		return axle.transform.localRotation.eulerAngles.z;
	}

	float setSpeed ()
	{
		return speed = (getAngle () / 100 * 2) * maxspeed;
	}

	void Update ()
	{
		getAngle ();
		setSpeed ();
		if (getRotation () < 90) {
			this.transform.Rotate (Vector3.up * Time.deltaTime * rotationspeed);
			hinge.connectedAnchor += transform.forward * Time.deltaTime * speed;
			wheelhinge.connectedAnchor += transform.forward * Time.deltaTime * rotationspeed;
		} else {
			this.transform.Rotate (Vector3.down * Time.deltaTime * rotationspeed);
			hinge.connectedAnchor -= transform.forward * Time.deltaTime * speed;
			wheelhinge.connectedAnchor -= transform.forward * Time.deltaTime * rotationspeed;
		}
		if (getAngle () < 90) {
			this.transform.position += transform.right * Time.deltaTime * speed;
			hinge.connectedAnchor += transform.right * Time.deltaTime * speed;
			wheelhinge.connectedAnchor += transform.right * Time.deltaTime * speed;
		} else {
			this.transform.position -= transform.right * Time.deltaTime * speed;
			hinge.connectedAnchor -= transform.right * Time.deltaTime * speed;
			wheelhinge.connectedAnchor -= transform.right * Time.deltaTime * speed;
		}
	}
}

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
		return speed = maxspeed;
	}

	void Update ()
	{
		getAngle ();
		setSpeed ();
		if (getRotation () > 20f && getRotation () < 90f) {
			this.transform.Rotate (Vector3.up * Time.deltaTime * rotationspeed);

			hinge.connectedAnchor += transform.forward * Time.deltaTime * speed;
			wheelhinge.connectedAnchor += transform.forward * Time.deltaTime * rotationspeed;

			//Use Quaternions to adjust the Rotation of the Hinged Objects
		} else if (getRotation () < 340f && getRotation () > 270f) {
			this.transform.Rotate (Vector3.down * Time.deltaTime * rotationspeed);
			hinge.connectedAnchor -= transform.forward * Time.deltaTime * speed;
			wheelhinge.connectedAnchor -= transform.forward * Time.deltaTime * rotationspeed;
		}

		//print ("Rotation: " + getRotation ());

		if (getAngle () < 340f && getAngle () > 270f) {
			this.transform.position += transform.right * speed / (Time.deltaTime * 1000f);
			hinge.connectedAnchor += transform.right * speed / (Time.deltaTime * 1000f);
			wheelhinge.connectedAnchor += transform.right * speed / (Time.deltaTime * 1000f);
		} else if (getAngle () > 20f && getAngle () < 90f) {
			this.transform.position -= transform.right * speed / (Time.deltaTime * 1000f);
			hinge.connectedAnchor -= transform.right * speed / (Time.deltaTime * 1000f);
			wheelhinge.connectedAnchor -= transform.right * speed / (Time.deltaTime * 1000f);
		}

		//print ("Angle: " + getAngle ());
	}
}

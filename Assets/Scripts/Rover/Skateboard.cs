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

	public float rotationBuffer;

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

		if ((getAngle () < 360f - rotationBuffer && getAngle () > 270f) || (getAngle () > rotationBuffer && getAngle () < 90f)) {
			if (getRotation () > 20f && getRotation () < 90f) {
				this.transform.Rotate (Vector3.up * Time.deltaTime * rotationspeed);
				hinge.connectedAnchor += transform.forward * Time.deltaTime * speed;
				wheelhinge.connectedAnchor += transform.forward * Time.deltaTime * rotationspeed;
			} else if (getRotation () < 360f - rotationBuffer && getRotation () > 270f) {
				this.transform.Rotate (Vector3.down * Time.deltaTime * rotationspeed);
				hinge.connectedAnchor -= transform.forward * Time.deltaTime * speed;
				wheelhinge.connectedAnchor -= transform.forward * Time.deltaTime * rotationspeed;
			}
		}

		//print ("Rotation: " + getRotation ());

		if (getAngle () < 360f - rotationBuffer && getAngle () > 270f) {
			this.transform.position += transform.right * speed / (Time.deltaTime * 1000f);
			hinge.connectedAnchor += transform.right * speed / (Time.deltaTime * 1000f);
			wheelhinge.connectedAnchor += transform.right * speed / (Time.deltaTime * 1000f);
		} else if (getAngle () > rotationBuffer && getAngle () < 90f) {
			this.transform.position -= transform.right * speed / (Time.deltaTime * 1000f);
			hinge.connectedAnchor -= transform.right * speed / (Time.deltaTime * 1000f);
			wheelhinge.connectedAnchor -= transform.right * speed / (Time.deltaTime * 1000f);
		}

		//print ("Angle: " + getAngle ());
	}
}

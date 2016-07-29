using UnityEngine;
using System.Collections;

public class Skateboard : MonoBehaviour
{
	public float speed;
	public float rotationspeed;

	public GameObject axle;
	public GameObject wheel;

	HingeJoint hinge;
	HingeJoint wheelhinge;

	bool canMoveForward;
	bool canMoveBackward;

	public float rotationBuffer;

	void Start ()
	{
		hinge = axle.GetComponent<HingeJoint> ();
		wheelhinge = wheel.GetComponent<HingeJoint> ();

		canMoveForward = RoverFrontCollider.canMoveForward;
		canMoveBackward = RoverBackCollider.canMoveBackwards;
	}

	float getRotation ()
	{
		return wheel.transform.localRotation.eulerAngles.z;
	}

	float getAngle ()
	{
		
		return axle.transform.localRotation.eulerAngles.z;
	}

	void Update ()
	{
		if ((getAngle () < 360f - rotationBuffer && getAngle () > 270f) || (getAngle () > 360f - rotationBuffer && getAngle () < rotationBuffer)) {
			if (getRotation () > rotationBuffer && getRotation () < 90f) {
				this.transform.Rotate (Vector3.up * Time.deltaTime * rotationspeed);
			} else if (getRotation () < 360f - rotationBuffer && getRotation () > 270f) {
				this.transform.Rotate (Vector3.down * Time.deltaTime * rotationspeed);
			}
		} else if (getAngle () > rotationBuffer && getAngle () < 90f) {
			if (getRotation () > rotationBuffer && getRotation () < 90f) {
				this.transform.Rotate (Vector3.down * Time.deltaTime * rotationspeed);
			} else if (getRotation () < 360f - rotationBuffer && getRotation () > 270f) {
				this.transform.Rotate (Vector3.up * Time.deltaTime * rotationspeed);
			}
		}


		/*if (canMoveForward) {
			if (getAngle () < 360f - rotationBuffer && getAngle () > 270f) {
				this.transform.position += transform.right * speed / (Time.deltaTime * 10000f);
			}
		}*/

		/*if (canMoveBackward) {
			if (getAngle () > rotationBuffer && getAngle () < 90f) {
				this.transform.position -= transform.right * speed / (Time.deltaTime * 10000f);
			}
		}*/
	}
}

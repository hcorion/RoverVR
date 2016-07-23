using UnityEngine;
using System.Collections;

public class Skateboard : MonoBehaviour
{
	public float speed = 0;
	public float maxspeed = 100;
	public int gear = 1;
	public int gearCount = 5;
	public HingeJoint hinge;
	int[] maxSpeedsperGear = new int[]{ 40, 70, 100, 130, 170 };
	public GameObject axle;
	public Vector3 eulerAngles;

	void Start ()
	{
		hinge = axle.GetComponent<HingeJoint> ();
	}

	float getAngle ()
	{
		return axle.transform.localRotation.eulerAngles.z;
	}

	float setSpeed ()
	{
		return speed = (getAngle () / 100 * 5) * maxspeed;
	}
	// Update is called once per frame
	void Update ()
	{
		setSpeed ();
		//Debug.Log ("The angle is " + getAngle ());
		if (getAngle () != 0) {
			Debug.Log ("The angle is not zero!");
		}
		if (getAngle () < 90) {
			this.transform.position += transform.right * Time.deltaTime * speed;
			hinge.connectedAnchor += transform.right * Time.deltaTime * speed;
		} else {
			this.transform.position -= transform.right * Time.deltaTime * speed;
			hinge.connectedAnchor -= transform.right * Time.deltaTime * speed;
		}
	}
}

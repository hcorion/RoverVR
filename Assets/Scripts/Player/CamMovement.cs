using UnityEngine;
using System.Collections;

public class CamMovement : MonoBehaviour
{
	public float sensitivityY;
	public float maxRotation;
	public float minRotation;

	float currentRotation;

	void Start ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update ()
	{
		float rotateY = Input.GetAxis ("Mouse Y") * -sensitivityY;
		currentRotation += rotateY;
		currentRotation = Mathf.Clamp (currentRotation, minRotation, maxRotation);
		transform.localRotation = Quaternion.identity;
		transform.Rotate (new Vector3 (currentRotation, 0, 0), Space.Self);
	}
}

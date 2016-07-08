using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public bool inverX = false;
	public bool inverY = false;

	public int cameraSensitivity=50;

	public int maxAngleUp = 90;
	public int maxAngleDown = -90;
	public float currentAngle;

	// Use this for initialization
	void Start () {
				currentAngle = Camera.main.transform.localEulerAngles.x;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;




	}
	
	// Update is called once per frame
	void Update () {
	
	}
					void UpdateCamera() {
						float dx = Input.GetAxis("Mouse X");
						float dy = -Input.GetAxis("Mouse Y");

						dx *= cameraSensitivity;
						dy *= cameraSensitivity;

						if(inverX) {
							dx = -dx;
						}
						if(inverY) {
							dy = -dy;
						}

						transform.Rotate(0, dx * Time.deltaTime, 0);

						currentAngle += dy * Time.deltaTime;
						currentAngle = Mathf.Clamp(currentAngle, maxAngleDown, maxAngleUp);

						Vector3 rotation = Camera.main.transform.localEulerAngles;
						rotation.x = currentAngle;
						Camera.main.transform.localEulerAngles = rotation;
					}
}

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody rb;

	public float speed;
	public float sensitivityX;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		//Move
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Vector3 move = new Vector3 (h, rb.velocity.y, v).normalized * speed;
		move = transform.TransformVector (move);

		rb.velocity = move;

		//Rotate X
		float rotateX = Input.GetAxis ("Mouse X") * sensitivityX;
		transform.Rotate (0, rotateX, 0);
	}
}

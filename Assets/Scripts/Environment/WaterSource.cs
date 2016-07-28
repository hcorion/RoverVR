using UnityEngine;
using System.Collections;

public class WaterSource : MonoBehaviour
{
	public float waterRadius;
	public float minWaterForLife;
	public float moistureContent;

	private Transform radiusIndicator;

	public float offsetY;
	public GameObject arrowPrefab;

	GameObject arrow;

	public Transform player;
	public float playerDisplacementBuffer;

	public bool found = false;

	void Start ()
	{
		radiusIndicator = transform;
		radiusIndicator.localScale = new Vector3 ((float)waterRadius, 0.005f, (float)waterRadius);

		Vector3 arrowPosition = new Vector3 (transform.position.x, transform.position.y + offsetY, transform.position.z);

		arrow = (GameObject)Instantiate (arrowPrefab, arrowPosition, Quaternion.identity);
	}

	void Update ()
	{
		Vector3 vector = player.position - arrow.transform.position;
		vector.x = vector.z = 0.0f;
		arrow.transform.LookAt (player.position - vector);
		arrow.transform.Rotate (90, 90, 90);

		Vector3 arrowLocation = gameObject.transform.position + player.position.normalized * waterRadius / 2f;
		arrow.transform.position = new Vector3 (arrowLocation.x, arrow.transform.position.y, arrowLocation.z);

		Vector3 playerDisplacement = player.position - arrowLocation;

		if (playerDisplacement.magnitude <= playerDisplacementBuffer) {
			arrow.layer = LayerMask.NameToLayer ("Water");
		} else if (playerDisplacement.magnitude > playerDisplacementBuffer) {
			arrow.layer = LayerMask.NameToLayer ("Arrow");
		}
	}
}
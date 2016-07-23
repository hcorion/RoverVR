using UnityEngine;
using System.Collections;

public class WaterSource : MonoBehaviour
{
	public float waterRadius;
	public float minWaterForLife;
	public float moistureContent;

	private Transform radiusIndicator;

	public Camera cameraToLookAt;
	public float offsetY;
	public GameObject arrowPrefab;

	GameObject arrow;

	void Start ()
	{
		radiusIndicator = transform;
		radiusIndicator.localScale = new Vector3 ((float)waterRadius, 0.005f, (float)waterRadius);

		Vector3 arrowPosition = new Vector3 (transform.position.x, transform.position.y + offsetY, transform.position.z);

		arrow = (GameObject)Instantiate (arrowPrefab, arrowPosition, Quaternion.identity);
	}

	void Update ()
	{
		Vector3 vector = cameraToLookAt.transform.position - arrow.transform.position;
		vector.x = vector.z = 0.0f;
		arrow.transform.LookAt (cameraToLookAt.transform.position - vector);
		arrow.transform.Rotate (90, 90, 90);
	}
}
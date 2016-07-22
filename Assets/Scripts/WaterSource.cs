using UnityEngine;
using System.Collections;

public class WaterSource : MonoBehaviour
{
	public float waterRadius;
	public float minWaterForLife;
	public float moistureContent;

	private Transform radiusIndicator;

	public Camera cameraToLookAt;
	public GameObject arrow;

	void Start ()
	{
		radiusIndicator = transform;
		radiusIndicator.localScale = new Vector3 ((float)waterRadius, 0.005f, (float)waterRadius);
	}

	void Update ()
	{
		Vector3 vector = cameraToLookAt.transform.position - arrow.transform.position;
		vector.x = vector.z = 0.0f;
		arrow.transform.LookAt (cameraToLookAt.transform.position - vector);
		arrow.transform.Rotate (0, 180, 0);
	}
}
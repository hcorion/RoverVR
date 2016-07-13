using UnityEngine;
using System.Collections;

public class WaterSource : MonoBehaviour
{
	public float waterRadius;
	public float minWaterForLife;
	public float moistureContent;

	private Transform radiusIndicator;

	void Start ()
	{
		radiusIndicator = transform;
		radiusIndicator.localScale = new Vector3 ((float)waterRadius, 0.005f, (float)waterRadius);
	}
}
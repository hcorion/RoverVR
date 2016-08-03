using UnityEngine;
using System.Collections;

public class lightDepleter : MonoBehaviour {
	private Light pointLight;

	// Use this for initialization
	void Start () {
		pointLight = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		pointLight.intensity -= Time.deltaTime * 8f;
		if (pointLight.intensity <= 0.0f)
		{
			Object.Destroy(gameObject);
		}	
	}
}
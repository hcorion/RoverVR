using UnityEngine;
using System.Collections;

public class Sandstorm : MonoBehaviour {
	public ParticleSystem system;
	public float time = 0;
	float x;
	float y;
	float z;
	ParticleSystem.MinMaxCurve rate;
	float unit = 20/3;
	float c;
	bool i = true;
	// Use this for initialization
	//terrain is 500 by 500, height doesn't matter
	//currently reaches 500 by 500 in 1 minute
	void Start () {
		system = GetComponent<ParticleSystem>();
		x = system.shape.box.x;
		y = system.shape.box.y;
		z = system.shape.box.z;
		c = system.startSize;
		Debug.Log (c);
	}
	
	// Update is called once per frame

	void Update () {
		if (i) {
			var a = system.shape;
			var b = system.emission;
			system.startSize = c;
			x += Time.deltaTime * unit;
			z += Time.deltaTime * unit;
			rate = b.rate;
			rate.constantMax += 1f;
			rate.constantMin += 1f;
			b.rate = rate;
			c += 0.05f;
			a.box = new Vector3 (x, y, z);
		}
		if (x > 500) {
			i = false;
		}
	}
}

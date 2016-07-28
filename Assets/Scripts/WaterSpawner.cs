using UnityEngine;
using System.Collections;

public class WaterSpawner : MonoBehaviour {

	public GameObject watersource;
	public GameObject prefab;
	public Vector3 holeLocalPos = Vector3.zero;
	public float holeRadius = 100f;
	public float diskRadius = 175f;
	//ArrayList angles = new ArrayList();
	void Start() { 
		float randomangle1 = Random.Range(10f, 50f);
		float randomangle2 = Random.Range (70f, 110f);
		float randomangle3 = Random.Range (130f, 170f);
		float randomangle4 = Random.Range (190f, 230f);
		float randomangle5 = Random.Range (310f, 350f);
	//	angles.Add (randomangle1);
	//	angles.Add (randomangle2);
	//	angles.Add (randomangle4);
	//	angles.Add (randomangle5);
		float[] angles = new float[5] { randomangle1, randomangle2, randomangle3, randomangle4, randomangle5 };
		for (int i = 0; i < 5; i++) {
			float randomdist = Random.Range(100f, 120f);
			GameObject water = (GameObject) Instantiate (watersource, new Vector3 (250, 0, 250), transform.rotation);
			water.transform.Rotate(new Vector3(0, angles[i], 0));
			water.transform.Translate(new Vector3(randomdist, 0, randomdist));
			//transform.position = new Vector3(Mathf.Sqrt(randomdist/2f), 0, (Mathf.Sqrt(randomdist/2f)));
		}
	}
		
	void Spawner() {
		Vector3 pos = FindPos();
		pos += transform.position;
		Instantiate(prefab, pos, transform.rotation);
	}

	Vector3 FindPos() {
		Vector3 pos = Vector3.zero;
		for (var i = 0; i < 1000; i++) {
			pos = Random.insideUnitCircle * diskRadius;
			if (Vector3.Distance(pos, holeLocalPos) > holeRadius) {
				return pos;
			}
		}
		return pos;
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}

using UnityEngine;
using System.Collections;

public class RockSpawning : MonoBehaviour
{
	public Transform positionOne;
	public Transform positionTwo;
	private float minTerrainHeight;

	public GameObject cameraRig;

	public Terrain terrain;

	public GameObject[] rocks;

	public float maxRockPercentage = 100;
	public int rocksToSpawn = 500;

	void Awake ()
	{
		minTerrainHeight = terrain.SampleHeight (cameraRig.transform.position);
	}
	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i <= rocksToSpawn; i++) {
			Vector3 randomPos = createRandomPos ();
			if (terrain.SampleHeight (randomPos) == minTerrainHeight) {
				GameObject rock = (GameObject)Instantiate (rocks [Random.Range (0, rocks.Length)], randomPos, Quaternion.identity);
				//rock.GetComponent<Rigidbody>().detectCollisions = false;
				//rock.GetComponent<Rigidbody>().Sleep();
				StartCoroutine (waitForIt (rock));
				ObjectProperties objectProperties = rock.GetComponent<ObjectProperties> ();
				if (objectProperties == null) {
					//Debug.Log ("The rock " + rock.name + " Doesn't have objectProperties! I'll add it.");
					objectProperties = rock.AddComponent<ObjectProperties> ();
				}
				float rockValues = maxRockPercentage + 30;
				objectProperties.rockPercentage [0] = Random.Range (10, 50);
				objectProperties.rockPercentage [1] = Random.Range (10, 50);
				objectProperties.rockPercentage [2] = Random.Range (10, 50);
				objectProperties.enabled = false;
			}
			//Instantiate(rocks[Random.Range(0, rocks.Length)], new Vector3(Random.Range(positionOne.x, positionTwo.x), Random.Range(positionOne.y, positionTwo.y), Random.Range(positionOne.z, positionTwo.z)), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	Vector3 createRandomPos ()
	{
		return new Vector3 (Random.Range (positionOne.position.x, positionTwo.position.x), minTerrainHeight + .5f, Random.Range (positionOne.position.x, positionTwo.position.x));
	}

	IEnumerator waitForIt (GameObject rock)
	{
		yield return new WaitForSeconds (.5f);
		//rock.GetComponent<Rigidbody> ().detectCollisions = false;
		rock.GetComponent<Rigidbody> ().isKinematic  = true;
	}
}
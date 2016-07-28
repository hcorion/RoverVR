using UnityEngine;
using System.Collections;

public class RockSpawning : MonoBehaviour {
	public Vector3 positionOne;
	public Vector3 positionTwo;
	private float minTerrainHeight;

	public GameObject cameraRig;

	public Terrain terrain;

	public GameObject[] rocks;
	public int rocksToSpawn = 500;
	void Awake()
	{
		minTerrainHeight = terrain.SampleHeight(cameraRig.transform.position);
	}
	// Use this for initialization
	void Start () 
	{
		for( int i = 0; i <= rocksToSpawn; i++)
		{
			Vector3 randomPos = createRandomPos();
			if (terrain.SampleHeight(randomPos) == minTerrainHeight)
			{
				GameObject rock = (GameObject)Instantiate(rocks[Random.Range(0, rocks.Length)],randomPos, Quaternion.identity);
				rock.GetComponent<Rigidbody>().detectCollisions = false;
			}
			//Instantiate(rocks[Random.Range(0, rocks.Length)], new Vector3(Random.Range(positionOne.x, positionTwo.x), Random.Range(positionOne.y, positionTwo.y), Random.Range(positionOne.z, positionTwo.z)), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	Vector3 createRandomPos()
	{
		return new Vector3(Random.Range(positionOne.x, positionTwo.x), minTerrainHeight + 1f, Random.Range(positionOne.x, positionTwo.x));
	}
	IEnumerator waitForIt(GameObject rock)
	{
		yield return new WaitForSeconds(.01f);
		rock.GetComponent<Rigidbody>().detectCollisions = false;
	}
}
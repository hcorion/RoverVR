using UnityEngine;
using System.Collections;

public class RockSpawning : MonoBehaviour {
	public Vector3 positionOne;
	public Vector3 positionTwo;

	public GameObject[] rocks;
	public int rocksToSpawn = 500;

	// Use this for initialization
	void Start () 
	{
		for( int i = 0; i <= rocksToSpawn; i++)
		{
			Instantiate(rocks[Random.Range(0, rocks.Length)], new Vector3(Random.Range(positionOne.x, positionTwo.x), Random.Range(positionOne.y, positionTwo.y), Random.Range(positionOne.z, positionTwo.z)), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

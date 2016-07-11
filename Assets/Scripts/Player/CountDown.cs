using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {
	public float maxTime = 600; //Because it is in seconds;
	private float curTime;
	private string prettyTime;
	// Use this for initialization
	void Start () {
		curTime = maxTime;
	}
	
	// Update is called once per frame
	void Update () {
		curTime -= Time.deltaTime;
		//Debug.Log(curTime);
		prettyTime = Mathf.FloorToInt(curTime/60) + ":" + Mathf.FloorToInt((60*(curTime/60 - Mathf.Floor(curTime/60))));
		Debug.Log(prettyTime);
	}
}

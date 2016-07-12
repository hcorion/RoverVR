using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
	public float maxTime = 600;
	//Because it is in seconds;
	private float curTime;
	private string prettyTime;
	private Text text;
	private GameObject gameController;
	// Use this for initialization
	void Start ()
	{
		curTime = maxTime;
		text = gameObject.GetComponent (typeof(Text)) as Text;
		gameController = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update ()
	{
		curTime -= Time.deltaTime;
		//Debug.Log(curTime);
		prettyTime = Mathf.FloorToInt (curTime / 60) + ":" + Mathf.FloorToInt ((60 * (curTime / 60 - Mathf.Floor (curTime / 60))));
		//Debug.Log(prettyTime);
		text.text = prettyTime;
		if (curTime <= 0) {
			gameController.GetComponent<GameManager> ().GameOver ();
		}

	}
}

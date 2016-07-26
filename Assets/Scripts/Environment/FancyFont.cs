using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FancyFont : MonoBehaviour {
	public string fullText = "This is a Test";
	private Text text;
	private string[] words;

	// Use this for initialization
	void Awake()
	{
		text = gameObject.GetComponent<Text>();
		if (text == null)
			Debug.LogError("text is equal to null!");
	}
	void Start () {
		text.text = "";
		words = fullText.Split(' ');
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

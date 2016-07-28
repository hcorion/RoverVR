using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FancyFont : MonoBehaviour {
	public string fullText = "This is a Test sentence that is being typed totally dynamically using two foreach loops, some special strings and some amazingness.";
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
		StartCoroutine(printText());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator printText() {
        yield return new WaitForSeconds(0.5f);
		foreach(string word in words)
		{
			if (word == "\\n")
			{
				text.text += System.Environment.NewLine;
			}
			else
			{
			foreach(char c in word)
			{
				text.text += c;
				yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
			}
			text.text += " ";
			yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));	
			}
		}
    }
}

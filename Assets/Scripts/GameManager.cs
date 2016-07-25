using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public bool gameOver = false;
	public GameObject wonText;
	public string mainSceneName = "Main Scene";
	//public bool StartGame = false;

	void Awake ()
	{
		DontDestroyOnLoad (transform.gameObject);
		wonText.SetActive (false);
	}

	void Start ()
	{
	}

	void Update ()
	{
		if (gameOver) {
			//Application.Quit();
			wonText.SetActive (true);
		}
	}

	public void GameOver ()
	{
		gameOver = true;
	}

	public void StartGame ()
	{
		//Add a a fade effect.
		SceneManager.LoadScene(mainSceneName);
	}
}

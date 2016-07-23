using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public bool gameOver = false;
	public GameObject wonText;
	public string mainSceneName = "Main Scene";
	public int a = 0;

	void Awake ()
	{
		DontDestroyOnLoad (transform.gameObject);
		wonText.SetActive (false);
	}

	void Start ()
	{
		//SceneManager.LoadSceneAsync(mainSceneName, LoadSceneMode.Single);
		if(a = 0) {
				Application.LoadLevel(0);
		}
		

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
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public bool gameOver = false;
	public GameObject wonText;
	public string mainSceneName = "Main Scene";
	public int a = 0;
	private bool hasStartedGame = false;

	void Awake ()
	{
		DontDestroyOnLoad (transform.gameObject);
		wonText.SetActive (false);
	}

	void Start ()
	{
		//SceneManager.LoadSceneAsync(mainSceneName, LoadSceneMode.Single);
		if(SceneManager.GetActiveScene().ToString() == mainSceneName)
		{
			firstState();
		}
	}

	void Update ()
	{
		if (gameOver) {
			//Application.Quit();
			wonText.SetActive (true);
		}
		if(SceneManager.GetActiveScene().ToString() == mainSceneName)
		{
			if(hasStartedGame == false)
			{
				firstState();
				hasStartedGame = true;
			}
		}
	}

	public void GameOver ()
	{
		gameOver = true;
	}
	private void firstState()
	{

	}
	public string getMainSceneName()
	{
		return mainSceneName;
	}
}

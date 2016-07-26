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

	private bool hasThrownTool = false;
	private int stateIndex = 0;

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
		if(hasStartedGame == true)
		{
			if(stateIndex == 1)
			{

			}
			if(hasThrownTool == false && stateIndex <= 1)
			{

			}
		}
		 else if(SceneManager.GetActiveScene().ToString() == mainSceneName)
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
		/*
		So here the plan.
		We drop the user (not literally) into the world. 
		The user will have already figured out how to use trigger on the controller.
		We then give them text/speech guidence to pick up the tools
		*/
		stateIndex = 1;
	}
	public string getMainSceneName()
	{
		return mainSceneName;
	}
}
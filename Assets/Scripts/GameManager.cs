using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool gameOver = false;
	public GameObject wonText;
	public string mainSceneName = "Main Scene";
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
	// Use this for initialization
	void Start ()
	{
		SceneManager.LoadSceneAsync(mainSceneName, LoadSceneMode.Single);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver)
		{
			//Application.Quit();
			wonText.SetActive(true);
		}
	}
	public void GameOver()
	{
		gameOver = true;
	}
}

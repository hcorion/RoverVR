using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool gameOver = false;
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
	
	}
}

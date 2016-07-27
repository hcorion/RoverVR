using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public bool gameOver = false;
	public GameObject wonText;
	public string mainSceneName = "Main Scene";
	//public int a = 0;
	private bool hasStartedGame = false;

	private bool hasThrownTool = false;
	private int stateIndex = 0;

	private bool hasWonFirstState = false;
	//All of the tools to instantiate.
	public GameObject selfieStick;
	private GameObject newSS;
	public GameObject neutronDetector;
	private GameObject newND;
	public GameObject ChemCam;
	private GameObject newCC;
	public GameObject Binoculars;
	private GameObject newB;

	//The points at which to drop the tool. Should be all just empty gameObjects
	//Drops the Selfie Stick and NeutronDetector
	public GameObject firstDropPoint;
	//Drops the ChemCam
	public GameObject secondDropPoint;
	//Drops the Binoculars
	public GameObject thirdDropPoint;
	
	//Basic stuff for detection.
	public GameObject Player;
	public GameObject EasyObjectSpawn;
	public float toolResetRadius = 2;


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
			StartCoroutine(firstState());
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
				if (hasWonFirstState == true)
				{
					stateIndex = 2;
				}
			}
			if(stateIndex == 2)
			{
				if ()
			}
			if(hasThrownTool == false && stateIndex == 1)
			{
				bool notthrownTool = false;
				int toolIndex = 0;
				foreach (Collider col in Physics.OverlapSphere(Player.transform.position, toolResetRadius))
				{
					if (col.gameObject == neutronDetector)
					{
						notthrownTool = true;
						toolIndex = 1;
					}
					else if (col.gameObject == selfieStick)
					{
						notthrownTool = true;
						toolIndex = 2;
					}
				}
				if (notthrownTool == false)
				{
					if (toolIndex == 1)
					{
						//That's the neutron detector.
						Object.Destroy(newND);
						newND = (GameObject)Object.Instantiate(neutronDetector, EasyObjectSpawn.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
					}
					else if (toolIndex == 2)
					{
						//That's the SelfieStick
						newSS = (GameObject)Object.Instantiate(selfieStick, EasyObjectSpawn.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
						Object.Destroy(newND);
					}
					//Talk to the player.
				}
			}
		}
		 else if(SceneManager.GetActiveScene().ToString() == mainSceneName)
		{
			if(hasStartedGame == false)
			{
				StartCoroutine(firstState());
				hasStartedGame = true;
			}
		}
	}
	public void GameOver ()
	{
		gameOver = true;
	}


	private IEnumerator firstState()
	{
		/*
		So here the plan.
		We drop the user (not literally) into the world. 
		The user will have already figured out how to use trigger on the controller.
		We then give them text/speech guidence to pick up the tools
		*/
		stateIndex = 1;
		yield return new WaitForSeconds(2f);
		//Play sound
		//Show text? Possibly?
		//
		yield return new WaitForSeconds(3f);
		
		//Drop tools.
		newND = (GameObject)Object.Instantiate(neutronDetector, firstDropPoint.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
		newSS = (GameObject)Object.Instantiate(selfieStick, firstDropPoint.transform.position + new Vector3(-2, 0, 0), Quaternion.identity);

		yield return new WaitForSeconds(1f);
		//More sound / text introducing the water Source.
	}

	public void wonFirstState()
	{
		hasWonFirstState = true;
	}
	public string getMainSceneName()
	{
		return mainSceneName;
	}
}
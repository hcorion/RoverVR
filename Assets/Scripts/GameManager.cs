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
	
	//Selfie Stick
	public GameObject selfieStick;
	private GameObject newSS;
	//Neutron Detector
	public GameObject neutronDetector;
	private GameObject newND;
	//Chem Cam
	public GameObject ChemCam;
	private GameObject newCC;
	private NewtonVR.ChemCamController newCCcontroller;
	//Binoculars
	public GameObject Binoculars;
	private GameObject newB;
	//Camera slash Rover
	private GameObject CameraRig;
	public GameObject Rover;

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
	//For the second state
	private bool hasStartedSecondState = false;
	private bool hasPickedUpChemCam = false;


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
			StartCoroutine(FirstState());
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
				if (hasStartedSecondState == false)
				{
					StartCoroutine(SecondState());
					hasStartedSecondState = true;	
				} 
				else
				{
                    if (newCCcontroller.AttachedHand != null && hasPickedUpChemCam == false)
                    {
						//Spawn the rover in.
						hasPickedUpChemCam = true;
						Rover.transform.position = CameraRig.transform.position;
						CameraRig.transform.parent = Rover.transform.parent;
                    }
                }
			}
			if(hasThrownTool == false && stateIndex == 1)
			{
				bool notthrownTool = false;
				int toolIndex = 0;
				foreach (Collider col in Physics.OverlapSphere(CameraRig.transform.position, toolResetRadius))
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
				StartCoroutine(FirstState());
				hasStartedGame = true;
				CameraRig = GameObject.Find("NVRCameraRig");
				if (CameraRig == null)
				{
					Debug.Log("The object NVRCameraRig has not been found in the Main Scene.");
				}
			}
		}
	}
	public void GameOver ()
	{
		gameOver = true;
	}


	private IEnumerator FirstState()
	{
		/*
		So here's the plan.
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

	private IEnumerator SecondState()
	{
		/*
		So here's the plan.
		The user now knows how to use the tools and how to find life.
		We'll spawn in the chemcam in a corner, when the user goes to pick it up, we'll spawn the Rover.
		*/
		yield return new WaitForSeconds(1f);
		//tell the player the ChemCam has just been dropped in.
		//
		//
		newCC = (GameObject)Object.Instantiate(ChemCam, secondDropPoint.transform.position, Quaternion.identity);
		newCCcontroller = newCC.GetComponent<NewtonVR.ChemCamController>();
		if (newCCcontroller == null)
		{
			Debug.LogError("The instantiated ChemCam doesn't have a ChemCamController script");
		}
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
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

								        

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Blocks") ){
			new WaitForSeconds(1);
			Application.LoadLevel("Main Scene");

		}


	}
	}


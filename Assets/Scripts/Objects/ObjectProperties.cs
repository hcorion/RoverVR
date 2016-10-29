using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
//using System.Diagnostics;

public class ObjectProperties : MonoBehaviour
{
	/*If you add rocks. You need to change the following scripts:
	ChemCamController
	RepairerScript
	ChemLab
	RockSpawning

	*/
	private string[] rockContent = new string [] {"Unusable",
		"Aluminum", 
		"Copper"
	};
	public float[] rockPercentage = new float [] {0.0f, 
		0.0f, 
		0.0f
	};
	//The seconds in time it takes for the material to break
	//0.0f is equal to unbreakable.
	float[] rockBreakage = new float [] {0.0f,
		2.0f,
		3.0f
	};

	Light pointLight;

	public static bool isBroken = false;
	public bool isRockBreak = false;
	public float lightIntensity = 2.0f;

	void Start ()
	{
		pointLight = GetComponentInChildren<Light> ();
	}

    /*void Update()
    {
        if (isRockBreak ==false)
        
            if (pointLight.intensity != 0f)
            {
                
            }
        }
    }*/

	public string getSimpleMaterial ()
	{
		float maxValue = 0;
		int i = 0;
		int maxIndex = 0;
		foreach (float d in rockPercentage) {
			if (d >= maxValue) {
				maxValue = d;
				maxIndex = i;
			}
			i++;
		}
		return rockContent [maxIndex];
	}

	public Dictionary <string, float> getMaterialDictionary ()
	{
		Dictionary<string, float> elements = new Dictionary<string, float> ();

		for (int i = 0; i < rockPercentage.Length; ++i) {
			if (rockPercentage [i] > 0f) {
				elements.Add (rockContent [i], rockPercentage [i]);
			}
		}

		return elements;
	}

	public ArrayList getMaterialNames ()
	{
		ArrayList elementNames = new ArrayList ();

		for (int i = 0; i < rockPercentage.Length; ++i) {
			if (rockPercentage [i] > 0f) {
				elementNames.Add (rockContent [i]);
			}
		}

		return elementNames;
	}

	public float getSimpleRockBreakage ()
	{
		int i = 0;
		foreach (string s in rockContent) {
			if (s == getSimpleMaterial ()) {
				return rockBreakage [i];
			}
			i++;
		}
		Debug.LogError ("Hmmm, something went wrong in getSimpleRockBreakage");
		return 0.0f;
	}

	public void breakRock ()
	{
		GameObject newRock = null;
		String newRockName = "";
		if (gameObject.name.StartsWith ("rock_d_01")) {
			newRockName = "rock-d-01-f";
		} else if (gameObject.name.StartsWith ("rock_d_02")) {
			newRockName = "rock-d-02-f";
			//newRock = (GameObject)Instantiate (Resources.Load ("Prefabs/Rocks/rock-d-02-f"), transform.position, transform.rotation);
		} else if (gameObject.name.StartsWith ("rock_d_03")) {
			newRockName = "rock-d-03-f";
			//newRock = (GameObject)Instantiate (Resources.Load ("Prefabs/Rocks/rock-d-03-f"), transform.position, transform.rotation);
		} else if (gameObject.name.StartsWith ("rock_d_05")) {
			newRockName = "rock-d-05-f";
			//newRock = (GameObject)Instantiate (Resources.Load ("Prefabs/Rocks/rock-d-05-f"), transform.position, transform.rotation);
		} else if (gameObject.name.StartsWith ("rock_d_06")) {
			newRockName = "rock-d-06-f";
			//newRock = (GameObject)Instantiate (Resources.Load ("Prefabs/Rocks/rock-d-06-f"), transform.position, transform.rotation);
		} else if (gameObject.name.StartsWith ("Rock01")) {
			newRockName = "rock01-f";
			//newRock = (GameObject)Instantiate (Resources.Load ("Prefabs/Rocks/rock01-f"), transform.position, transform.rotation);
		} else if (gameObject.name.StartsWith ("Rock02")) {
			newRockName = "rock02-f";
			//newRock = (GameObject)Instantiate (Resources.Load ("Prefabs/Rocks/rock02-f"), transform.position, transform.rotation);
		} else {
			Debug.Log ("That object doesn't yet have a fancy breaking animation.");
			return;
		}
		newRock = (GameObject)Instantiate (Resources.Load ("Prefabs/Rocks/" + newRockName), transform.position, transform.rotation);
		gameObject.SetActive (false);
		ObjectProperties newRockProps = newRock.GetComponent<ObjectProperties> ();//.rockPercentage = rockPercentage;
		newRockProps.rockPercentage = rockPercentage;
		newRockProps.isRockBreak = true;
		isBroken = true;
        /*foreach (Transform child in newRock.transform)
        {
            ObjectProperties childObjectProps = child.transform.GetComponent<ObjectProperties>();
            if (child.transform.GetComponent<ObjectProperties>() != null)
            {
                childObjectProps.rockPercentage = newRockProps.rockPercentage;
                childObjectProps.isRockBreak = true;
            }
            else
            {
                childObjectProps = child.gameObject.AddComponent<ObjectProperties>();
                childObjectProps.rockPercentage = newRockProps.rockPercentage;
                childObjectProps.isRockBreak = true;
            }
            child.parent = null;
        }*/
        for (int p = 0; p < newRock.transform.childCount; p++)
        {
            GameObject child = newRock.transform.GetChild(p).gameObject;
            ObjectProperties childObjectProps = child.transform.GetComponent<ObjectProperties>();
            if (child.transform.GetComponent<ObjectProperties>() != null)
            {
                childObjectProps.rockPercentage = newRockProps.rockPercentage;
                childObjectProps.isRockBreak = true;
            }
            else
            {
                childObjectProps = child.gameObject.AddComponent<ObjectProperties>();
                childObjectProps.rockPercentage = newRockProps.rockPercentage;
                childObjectProps.isRockBreak = true;
            }
        }
        newRock.transform.DetachChildren();
		GameObject.Destroy(gameObject);
	}
}

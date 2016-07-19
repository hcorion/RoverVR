using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ObjectProperties : MonoBehaviour
{
	public string[] rockContent = new string [] {"Nil",
		"Aluminum", 
		"Copper"
	};
	public float[] rockPercentage = new float [] {0.0f, 
		2.0f, 
		1.0f
	};
	//The seconds in time it takes for the material to break
	//0.0f is equal to unbreakable.
	float[] rockBreakage = new float [] {0.0f,
		2.0f,
		3.0f
	};

	private Dictionary<string, float> elements = new Dictionary<string, float> ();

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

	public Dictionary <string, float> getComplexMaterial ()
	{
		return elements;
	}

	public float getSimpleRockBreakage ()
	{
		int i = 0;
		foreach (string s in rockContent) {
			Debug.Log (rockBreakage [i]);
			if (s == getSimpleMaterial ()) {
				return rockBreakage [i];
			}
			i++;
		}
		Debug.LogError ("Hmmm, something went wrong in getSimpleRockBreakage");
		return 0.0f;
	}
}

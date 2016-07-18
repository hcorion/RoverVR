using UnityEngine;
using System.Collections;

public class ObjectProperties : MonoBehaviour
{
	public string[] rockContent = new string [] {"Nil",
		"Aluminium", 
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
	public float getSimpleRockBreakage()
	{
		int i = 0;
		foreach(string s in rockContent)
		{
			if (s == rockContent[i])
			{
				return rockBreakage[i];
			}
			i++;
		}
		Debug.LogError("Hmmm, something went wrong in getSimpleRockBreakage");
		return 0.0f;
	}
}

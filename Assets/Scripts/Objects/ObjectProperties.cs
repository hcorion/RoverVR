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
			Debug.Log(rockBreakage[i]);
			if (s == getSimpleMaterial())
			{
				return rockBreakage[i];
			}
			i++;
		}
		Debug.LogError("Hmmm, something went wrong in getSimpleRockBreakage");
		return 0.0f;
	}

	public void breakRock()
	{
		if(gameObject.name.StartsWith("rock_d_01"))
		{
			gameObject.SetActive(false);
			Instantiate(Resources.Load("Prefabs/Rocks/rock-d-01-f"), transform.position, transform.rotation);
			Destroy(gameObject);
		}
		else
		{
			Debug.Log("That object doesn't yet have a fancy breaking animation.");
		}
	}
}

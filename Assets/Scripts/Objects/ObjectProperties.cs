using UnityEngine;
using System.Collections;

public class ObjectProperties : MonoBehaviour
{
	public string[] rockContent = new string [] 
	{"nil",
	 "Aluminium", 
	 "Copper"};
	 public float[] rockPercentage = new float []
	 {0.0f, 
	 0.0f, 
	 0.0f};

	public string getSimpleMaterial ()
	{
		float maxValue = 0;
		int i = 0;
		int maxIndex = 0;
		foreach(float d in rockPercentage)
		{
			if (d >= maxValue)
			{
				maxValue = d;
				maxIndex = i;
			}
			i++;
		}
		return rockContent[maxIndex];
	}
}

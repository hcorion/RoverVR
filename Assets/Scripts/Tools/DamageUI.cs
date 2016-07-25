using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageUI : MonoBehaviour
{
	public float maxHealth;
	public float startingHealth;
	public float health;

	public Slider slider;

	void Start ()
	{
		slider.maxValue = maxHealth;
		slider.minValue = 0;

		health = startingHealth;

		slider.value = health;
	}

	void Update ()
	{
		if (health < slider.minValue) {
			health = slider.minValue;
		}

		if (health > slider.maxValue) {
			health = slider.maxValue;
		}

		slider.value = health;
	}
}

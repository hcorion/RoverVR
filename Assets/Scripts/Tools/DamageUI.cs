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
		health = startingHealth;
	}

	void Update ()
	{
		slider.value = health;
	}
}

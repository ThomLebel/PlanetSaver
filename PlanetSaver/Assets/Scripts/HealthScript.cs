using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
	public float health;
	private float maxHealth;

	private void Start()
	{
		maxHealth = health;
	}

	public void AdjustHealth(float value)
	{
		health += value;

		if (health > maxHealth)
		{
			health = maxHealth;
		}
		if (health <= 0)
		{
			health = 0;
			Destroy(gameObject);
		}
	}

	public void AdjustMaxHealth(float value)
	{
		maxHealth += value;
	}
}

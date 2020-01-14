using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class HealthScript : MonoBehaviour
{
	public float health;
	private float maxHealth;

	private void Awake()
	{
		EventManager.StartListening(EventsNames.ActionEvent.DoDamage.ToString(), this.gameObject, AdjustHealth);
	}

	private void Start()
	{
		maxHealth = health;
	}

	public void AdjustHealth()
	{
		var eventData = EventManager.GetDataGroup(EventsNames.ActionEvent.DoDamage.ToString());

		if (eventData == null)
		{
			return;
		}

		GameObject target = eventData[0].ToGameObject();

		if (target != this.gameObject)
		{
			return;
		}

		health += eventData[1].ToFloat(); ;

		if (health > maxHealth)
		{
			health = maxHealth;
		}
		if (health <= 0)
		{
			health = 0;
			gameObject.SetActive(false);
		}
	}

	public void AdjustMaxHealth(float value)
	{
		maxHealth += value;
	}
}

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
		EventManager.StartListening(ConstantVar.DO_DAMAGE, TakeDamage);
		EventManager.StartListening(ConstantVar.HEAL, this.gameObject, Heal);
	}

	private void Start()
	{
		maxHealth = health;
	}

	private void TakeDamage()
	{
		var eventData = EventManager.GetIndexedDataGroup(ConstantVar.DO_DAMAGE);

		GameObject target = eventData.ToGameObject("target");

		if (target != this.gameObject)
		{
			return;
		}

		AdjustHealth(eventData.ToFloat("damage"));		
	}

	private void Heal(){
		var eventData = EventManager.GetDataGroup(ConstantVar.HEAL);

		if (eventData == null)
		{
			return;
		}

		GameObject target = eventData[0].ToGameObject();

		if (target != this.gameObject)
		{
			return;
		}

		AdjustHealth(eventData[1].ToFloat());
	}

	private void AdjustHealth(float value){
		health += value;

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

	private void AdjustMaxHealth(float value)
	{
		maxHealth += value;
	}
}

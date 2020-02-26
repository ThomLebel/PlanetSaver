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
		EventManager.StartListening(ConstantVar.TAKE_DAMAGE, TakeDamage);
		EventManager.StartListening(ConstantVar.HEAL, Heal);
	}

	private void Start()
	{
		maxHealth = health;
	}

	private void TakeDamage()
	{
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.TAKE_DAMAGE);
        if(sender == null || sender != gameObject){
            return;
        }

		AttackInfo attackInfo = (AttackInfo)EventManager.GetData(ConstantVar.TAKE_DAMAGE);
		AdjustHealth(attackInfo.damage * -1);
	}

	private void Heal(){
		var eventData = EventManager.GetIndexedDataGroup(ConstantVar.HEAL);

		GameObject target = eventData.ToGameObject("target");
		if (target != this.gameObject)
		{
			return;
		}

		AdjustHealth(eventData.ToFloat("value"));
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
			EventManager.EmitEvent(ConstantVar.IS_DEAD, gameObject);
		}
	}

	private void AdjustMaxHealth(float value)
	{
		maxHealth += value;
	}
}

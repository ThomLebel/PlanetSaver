using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[RequireComponent(typeof(InputManager))]
public class WeaponManager : MonoBehaviour
{
	public Weapon primaryWeapon;
	public List<string> targetsTag;

	public bool primaryAttack;

	private void Awake()
	{
		EventManager.StartListening(EventsNames.ActionEvent.UseAttack.ToString(), this.gameObject, UseWeapon);
	}

	private void Start()
	{
		primaryWeapon.targetTags = targetsTag;
	}

	// Update is called once per frame
	void Update()
    {
		if (primaryAttack)
		{
			primaryWeapon.Use(gameObject);
		}
	}

	private void UseWeapon()
	{
		var sender = EventManager.GetSender(EventsNames.ActionEvent.UseAttack.ToString());

		if (sender != null)
		{
			GameObject go = (GameObject)sender;

			if (go != gameObject)
			{
				return;
			}

			primaryAttack = EventManager.GetBool(EventsNames.ActionEvent.UseAttack.ToString());
		}
	}

	public void AddWeapon(string weapon)
	{
		if (primaryWeapon != null)
		{
			Destroy(primaryWeapon);
		}
		Type mType = Type.GetType(weapon);
		Component temp = gameObject.AddComponent(mType);
		primaryWeapon = temp as Weapon;

	}
}

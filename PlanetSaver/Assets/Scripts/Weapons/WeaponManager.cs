using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class WeaponManager : MonoBehaviour
{
	public Weapon primaryWeapon;
	public List<Weapon> skillList;

	private InputManager inputManager;

	private void Awake()
	{
		inputManager = GetComponent<InputManager>();
	}

	// Update is called once per frame
	void Update()
    {
		if (inputManager.primaryAttack)
		{
			primaryWeapon.Use(gameObject);
		}
		if (inputManager.skill1)
		{
			skillList[0].Use(gameObject);
		}
		if (inputManager.skill2)
		{
			skillList[1].Use(gameObject);
		}
		if (inputManager.skill3)
		{
			skillList[2].Use(gameObject);
		}
		if (inputManager.skill4)
		{
			skillList[3].Use(gameObject);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class WeaponManager : MonoBehaviour
{
	public List<Weapon> primaryWeapons;
	public List<string> targetsTag;

	public bool primaryAttack;

	private void Awake()
	{
		EventManager.StartListening(EventsNames.ActionEvent.UseAttack.ToString(), this.gameObject, UseWeapon);
	}

	private void Start()
	{
		foreach (Weapon weapon in primaryWeapons)
		{
			weapon.targetTags = targetsTag;
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (primaryAttack)
		{
			foreach (Weapon weapon in primaryWeapons)
			{
				weapon.Use(gameObject);
			}
		}
	}

	private void UseWeapon()
    {
        var sender = EventManager.GetSender(EventsNames.ActionEvent.UseAttack.ToString());

        if (sender == null)
        {
            return;
        }
        GameObject go = (GameObject)sender;

        if (go != gameObject)
        {
            return;
        }

        primaryAttack = EventManager.GetBool(EventsNames.ActionEvent.UseAttack.ToString());
    }

    public void AddWeapon(string weapon)
	{
		/*if (primaryWeapon != null)
		{
			Destroy(primaryWeapon);
		}
		Type mType = Type.GetType(weapon);
		Component temp = gameObject.AddComponent(mType);
		primaryWeapon = temp as Weapon;*/

	}
}

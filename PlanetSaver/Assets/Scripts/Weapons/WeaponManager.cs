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
	private bool canUseWeapon = true;

	private void Awake()
	{
		EventManager.StartListening(ConstantVar.USE_ATTACK, this.gameObject, UseWeapon);
        EventManager.StartListening(ConstantVar.BLOCK_MOVEMENT, BlockMovement);
	}

	private void Start()
	{
		foreach (Weapon weapon in primaryWeapons)
		{
			weapon.targetTags = targetsTag;
			weapon.owner = gameObject;
		}
	}

	// Update is called once per frame
	void Update()
    {
		if(!canUseWeapon){
			return;
		}

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
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.USE_ATTACK);

        if (sender == null || sender != gameObject)
        {
            return;
        }

        primaryAttack = EventManager.GetBool(ConstantVar.USE_ATTACK);
    }

    private void BlockMovement(){
        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.BLOCK_MOVEMENT);
        GameObject target = eventData.ToGameObject("target");
        if(target != gameObject){
            return;
        }

        canUseWeapon = eventData.ToBool("canMove");
    }
}

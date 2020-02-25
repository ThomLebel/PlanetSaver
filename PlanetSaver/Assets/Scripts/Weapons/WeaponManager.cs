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

	private List<string> originalTargets;

	private void Awake()
	{
		EventManager.StartListening(ConstantVar.USE_ATTACK, this.gameObject, UseWeapon);
        EventManager.StartListening(ConstantVar.BLOCK_MOVEMENT, BlockMovement);
		EventManager.StartListening(ConstantVar.MIND_CONTROL, MindControl);
		EventManager.StartListening(ConstantVar.RESET_MIND_CONTROL, ResetMindControl);
	}

	private void Start()
	{
		AssignTargetToWeapon();

		originalTargets = new List<string>();
		originalTargets = targetsTag;
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

	private void AssignTargetToWeapon(){
		foreach (Weapon weapon in primaryWeapons)
		{
			weapon.targetTags = targetsTag;
			weapon.owner = gameObject;
		}
	}

    private void BlockMovement(){
        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.BLOCK_MOVEMENT);
        GameObject target = eventData.ToGameObject("target");
        if(target != gameObject){
            return;
        }

        canUseWeapon = eventData.ToBool("canMove");
    }

	private void MindControl(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.MIND_CONTROL);
		if(sender == null || sender != gameObject){
			return;
		}

		primaryAttack = false;
		targetsTag = new List<string>();
		targetsTag.Add(EventManager.GetString(ConstantVar.MIND_CONTROL));
		AssignTargetToWeapon();
	}

	private void ResetMindControl(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.RESET_MIND_CONTROL);
		if(sender == null || sender != gameObject){
			return;
		}

		primaryAttack = false;
		targetsTag = originalTargets;
		AssignTargetToWeapon();
	}
}

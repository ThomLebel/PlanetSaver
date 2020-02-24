using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class InputManager : MonoBehaviour
{
	private void Update() {

		if(Input.GetButton("Jump")){
			ActivatePrimaryAttack(true);
		}
		if(Input.GetButtonUp("Jump")){
			ActivatePrimaryAttack(false);
		}
	}

	public void ActivatePrimaryAttack(bool value)
	{	
		EventManager.SetData(ConstantVar.USE_ATTACK, value);
		EventManager.EmitEvent(ConstantVar.USE_ATTACK, "tag:Player",  0f, this.gameObject);
	}

	public void ActivateSkill1(bool value)
	{
		EventManager.SetDataGroup(ConstantVar.USE_SKILL, "Skill1", value);
		EventManager.EmitEvent(ConstantVar.USE_SKILL, "tag:Player", 0f, this.gameObject);
	}

	public void ActivateSkill2(bool value)
	{
		EventManager.SetDataGroup(ConstantVar.USE_SKILL, "Skill2", value);
		EventManager.EmitEvent(ConstantVar.USE_SKILL, "tag:Player", 0f, this.gameObject);
	}

	public void ActivateSkill3(bool value)
	{
		EventManager.SetDataGroup(ConstantVar.USE_SKILL, "Skill3", value);
		EventManager.EmitEvent(ConstantVar.USE_SKILL, "tag:Player", 0f, this.gameObject);
	}

	public void ActivateSkill4(bool value)
	{
		EventManager.SetDataGroup(ConstantVar.USE_SKILL, "Skill4", value);
		EventManager.EmitEvent(ConstantVar.USE_SKILL, "tag:Player", 0f, this.gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class InputManager : MonoBehaviour
{

	public void ActivatePrimaryAttack(bool value)
	{
		EventManager.SetData(EventsNames.ActionEvent.UseAttack.ToString(), value);
		EventManager.EmitEvent(EventsNames.ActionEvent.UseAttack.ToString(), "tag:Player",  0f, this.gameObject);
	}

	public void ActivateSkill1(bool value)
	{
		EventManager.SetDataGroup(EventsNames.ActionEvent.UseSkill.ToString(), "Skill1", value);
		EventManager.EmitEvent(EventsNames.ActionEvent.UseSkill.ToString(), "tag:Player", 0f, this.gameObject);
	}

	public void ActivateSkill2(bool value)
	{
		EventManager.SetDataGroup(EventsNames.ActionEvent.UseSkill.ToString(), "Skill2", value);
		EventManager.EmitEvent(EventsNames.ActionEvent.UseSkill.ToString(), "tag:Player", 0f, this.gameObject);
	}

	public void ActivateSkill3(bool value)
	{
		EventManager.SetDataGroup(EventsNames.ActionEvent.UseSkill.ToString(), "Skill3", value);
		EventManager.EmitEvent(EventsNames.ActionEvent.UseSkill.ToString(), "tag:Player", 0f, this.gameObject);
	}

	public void ActivateSkill4(bool value)
	{
		EventManager.SetDataGroup(EventsNames.ActionEvent.UseSkill.ToString(), "Skill4", value);
		EventManager.EmitEvent(EventsNames.ActionEvent.UseSkill.ToString(), "tag:Player", 0f, this.gameObject);
	}
}

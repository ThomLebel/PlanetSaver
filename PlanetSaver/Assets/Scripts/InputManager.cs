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
		if(Input.GetButtonUp("skill1")){
			ActivateSkill(0);
		}
		if(Input.GetButtonUp("skill2")){
			ActivateSkill(1);
		}
		if(Input.GetButtonUp("skill3")){
			ActivateSkill(2);
		}
		if(Input.GetButtonUp("skill4")){
			ActivateSkill(3);
		}
	}

	public void ActivatePrimaryAttack(bool value)
	{	
		EventManager.SetData(ConstantVar.USE_ATTACK, value);
		EventManager.EmitEvent(ConstantVar.USE_ATTACK, this.gameObject);
	}

	public void ActivateSkill(int id)
	{
		EventManager.SetData(ConstantVar.USE_SKILL, id);
		EventManager.EmitEvent(ConstantVar.USE_SKILL, this.gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public bool primaryAttack;
	public bool skill1;
	public bool skill2;
	public bool skill3;
	public bool skill4;

	public void ActivatePrimaryAttack(bool value)
	{
		primaryAttack = value;
	}

	public void ActivateSkill1(bool value)
	{
		skill1 = value;
	}

	public void ActivateSkill2(bool value)
	{
		skill2 = value;
	}

	public void ActivateSkill3(bool value)
	{
		skill3 = value;
	}

	public void ActivateSkill4(bool value)
	{
		skill4 = value;
	}
}

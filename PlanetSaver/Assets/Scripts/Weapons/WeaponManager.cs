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
			primaryWeapon.Use();
		}
		if (inputManager.skill1)
		{
			skillList[0].Use();
		}
		if (inputManager.skill2)
		{
			skillList[1].Use();
		}
		if (inputManager.skill3)
		{
			skillList[2].Use();
		}
		if (inputManager.skill4)
		{
			skillList[3].Use();
		}
	}

	
}

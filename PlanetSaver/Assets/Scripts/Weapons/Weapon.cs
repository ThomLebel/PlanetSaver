using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public string weaponName;

	public virtual void Use()
	{
		Debug.Log("You used the " + weaponName);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public string weaponName;

	public List<string> targetTags = new List<string>();

	public virtual void Use(GameObject user)
	{
		Debug.Log("You used the " + weaponName);
	}
}

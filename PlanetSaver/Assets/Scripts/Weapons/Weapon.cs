using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public string weaponName;
	public List<string> targetTags = new List<string>();
	public GameObject owner;

	public bool weaponActive;

	public virtual void Use(GameObject user)
	{
		if(!weaponActive){
			weaponActive = true;
		}
	}
}

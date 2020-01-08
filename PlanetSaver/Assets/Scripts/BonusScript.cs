using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
	public string bonusToGet;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Debug.Log("Get a new Skill");
			collision.GetComponent<WeaponManager>().AddWeapon(bonusToGet);
			Destroy(gameObject);
		}
	}
}

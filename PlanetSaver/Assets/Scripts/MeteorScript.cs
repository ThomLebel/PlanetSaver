using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
	public int healthPoints;
	public float damageToPlayer;
	public float damageToPlanet;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Debug.Log("This meteor hit the player");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
	public float collisionDamage;
	public List<string> targetsTag;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		HandleCollision(collision.transform);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		HandleCollision(collision.transform);
	}

	private void HandleCollision(Transform target)
	{
		foreach (string tag in targetsTag)
		{
			if (target.CompareTag(tag))
			{
				Debug.Log("hit : " + tag);
				target.GetComponent<HealthScript>().AdjustHealth(collisionDamage);
				Destroy(gameObject);
			}
		}
	}
}

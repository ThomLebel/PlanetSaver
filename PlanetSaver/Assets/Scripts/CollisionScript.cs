using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

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
		Debug.Log("Send collision event");
		foreach (string tag in targetsTag)
		{
			if (target.CompareTag(tag))
			{
				Debug.Log("hit : " + tag);

				EventManager.SetDataGroup(EventsNames.ActionEvent.DoDamage.ToString(), target.gameObject, collisionDamage);
				EventManager.EmitEvent(EventsNames.ActionEvent.DoDamage.ToString(), this.gameObject);

				Destroy(gameObject);
			}
		}
	}
}

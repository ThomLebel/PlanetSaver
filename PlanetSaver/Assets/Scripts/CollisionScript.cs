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
		if (target.CompareTag(gameObject.tag))
		{
			return;
		}

		foreach (string tag in targetsTag)
		{
			if (target.CompareTag(tag))
			{
				EventManager.SetIndexedDataGroup(ConstantVar.DO_DAMAGE,
					new EventManager.DataGroup{id = "target", data = target.gameObject},
					new EventManager.DataGroup{id = "damage", data = collisionDamage}
				);
				EventManager.EmitEvent(ConstantVar.DO_DAMAGE, this.gameObject);

				Destroy(gameObject);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class CollisionScript : MonoBehaviour
{
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
				EventManager.SetData(ConstantVar.COLLIDE_WITH_SOMETHING, target.gameObject);
				EventManager.EmitEvent(ConstantVar.COLLIDE_WITH_SOMETHING, this.gameObject);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class CollisionScript : MonoBehaviour
{
	public float collisionDamage;
	public string attributes;
	public List<string> targetsTag;
	public GameObject initiator;

	void Awake() {
		if(initiator == null){
			initiator = gameObject;
		}
		attributes += ConstantVar.ATK_ATR_COLLISION+",";
	}

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
				AttackInfo attackInfo = new AttackInfo(
					initiator,
					gameObject,
					target.gameObject,
					collisionDamage,
					attributes
				);

				EventManager.SetData(ConstantVar.DO_DAMAGE, attackInfo);
				EventManager.EmitEvent(ConstantVar.DO_DAMAGE, this.gameObject);

				Destroy(gameObject);
			}
		}
	}
}

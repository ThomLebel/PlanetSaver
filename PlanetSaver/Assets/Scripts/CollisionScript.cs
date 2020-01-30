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
				EventManager.SetIndexedDataGroup(ConstantVar.DO_DAMAGE,
					new EventManager.DataGroup{id = "initiator", data = initiator},
					new EventManager.DataGroup{id = "target", data = target.gameObject},
					new EventManager.DataGroup{id = "damage", data = collisionDamage},
					new EventManager.DataGroup{id = "attributes", data = attributes}
				);
				EventManager.EmitEvent(ConstantVar.DO_DAMAGE, this.gameObject);
				Debug.Log("We hit something");

				Destroy(gameObject);
			}
		}
	}
}

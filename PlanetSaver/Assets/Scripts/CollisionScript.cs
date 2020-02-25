using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class CollisionScript : MonoBehaviour
{
	public List<string> targetsTag;

	private List<string> originalTargets;

	private void Start() {
		originalTargets = new List<string>();
		originalTargets = targetsTag;
		EventManager.StartListening(ConstantVar.MIND_CONTROL, MindControl);
		EventManager.StartListening(ConstantVar.RESET_MIND_CONTROL, ResetMindControl);
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
				EventManager.SetData(ConstantVar.COLLIDE_WITH_SOMETHING, target.gameObject);
				EventManager.EmitEvent(ConstantVar.COLLIDE_WITH_SOMETHING, this.gameObject);
			}
		}
	}

	private void MindControl(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.MIND_CONTROL);
		if(sender == null || sender != gameObject){
			return;
		}

		targetsTag = new List<string>();
		targetsTag.Add(EventManager.GetString(ConstantVar.MIND_CONTROL));
	}

	private void ResetMindControl(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.RESET_MIND_CONTROL);
		if(sender == null || sender != gameObject){
			return;
		}

		// targetsTag = new List<string>();
		targetsTag = originalTargets;
	}
}

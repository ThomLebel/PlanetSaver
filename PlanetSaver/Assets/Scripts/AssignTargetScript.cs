using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class AssignTargetScript : MonoBehaviour
{
	public string targetTag;
	public bool tauntable;
	public bool magnetizable;

	private Transform target;
	private string originalTarget;

	private void Start()
	{
		originalTarget = targetTag;
		EventManager.StartListening(ConstantVar.SET_PRIORITY_TARGET, AssignPriorityTarget);
		EventManager.StartListening(ConstantVar.RESET_PRIORITY_TARGET, ResetPriorityTarget);
		EventManager.StartListening(ConstantVar.MIND_CONTROL, MindControl);
		EventManager.StartListening(ConstantVar.RESET_MIND_CONTROL, ResetMindControl);
		EventManager.StartListening(ConstantVar.IS_DEAD, TargetIsDead);

		EventManager.EmitEvent(ConstantVar.FIND_PRIORITY_TARGET, gameObject);

		AssignTarget();		
	}

	private void Update() {
		if(target != null && target.CompareTag(gameObject.tag)){
			ResetTarget();
		}

		if(target != null){
			return;
		}
		AssignTarget();
	}

	private void AssignTarget(){
		if(target != null){
			return;
		}

		target = FindClosestTarget(targetTag, transform.position);

		if(target == null){
			return;
		}
		
		EventManager.SetData(ConstantVar.SET_TARGET, target);
		EventManager.EmitEvent(ConstantVar.SET_TARGET, this.gameObject);
		Debug.Log(target);
	}

	private void AssignPriorityTarget(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.SET_PRIORITY_TARGET);
		if(sender == null || sender == gameObject){
			return;
		}

		var eventData = EventManager.GetIndexedDataGroup(ConstantVar.SET_PRIORITY_TARGET);

		string type = eventData.ToString("type");
		Transform eventTarget = eventData.ToGameObject("target").transform; 
		switch(type){
			case ConstantVar.BUFF_TAUNT:
				if(tauntable){
					target = eventTarget;
				}
			break;
			case ConstantVar.BUFF_MAGNET:
				if(magnetizable){
					target = eventTarget;
				}
			break;
			default:
				AssignTarget();
			break;
		}

		if(target == null){
			return;
		}

		EventManager.SetData(ConstantVar.SET_TARGET, target);
		EventManager.EmitEvent(ConstantVar.SET_TARGET, this.gameObject);
	}

	private void ResetPriorityTarget(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.RESET_PRIORITY_TARGET);
		if(sender == null || sender == gameObject){
			return;
		}

		string eventType = EventManager.GetString(ConstantVar.RESET_PRIORITY_TARGET);
		switch(eventType){
			case ConstantVar.BUFF_TAUNT:
				if(!tauntable){
					return;
				}
			break;
			case ConstantVar.BUFF_MAGNET:
				if(!magnetizable){
					return;
				}
			break;
			default:
				return;
		}

		target = null;
		AssignTarget();
	}

	private void MindControl(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.MIND_CONTROL);
		if(sender == null || sender != gameObject){
			return;
		}

		targetTag = EventManager.GetString(ConstantVar.MIND_CONTROL);
		ResetTarget();
	}

	private void ResetMindControl(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.RESET_MIND_CONTROL);
		if(sender == null || sender != gameObject){
			return;
		}

		targetTag = originalTarget;
		ResetTarget();
	}

	private void TargetIsDead(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.IS_DEAD);
		if(sender == null || sender == gameObject || (target != null && sender != target.gameObject)){
			return;
		}

		ResetTarget();
	}

	private void ResetTarget(){
		target = null;
		AssignTarget();
	}

	private Transform FindClosestTarget(string targetTag, Vector3 position)
	{
		GameObject[] targetList = FindTargetArray(targetTag);

		Transform target = null;
		Transform temp = null;
		float dist = Mathf.Infinity;

		if (targetList.Length > 0)
		{
			target = targetList[0].transform;
			dist = (target.position - position).sqrMagnitude;

			for (int i=0; i<targetList.Length; i++)
			{
				temp = targetList[i].transform;

				Vector3 directionToTarget = temp.position - position;
				float dSqrToTarget = directionToTarget.sqrMagnitude;

				if (dSqrToTarget < dist)
				{
					dist = dSqrToTarget;
					target = targetList[i].transform;
				}
			}
			return target;
		}

		return null;
	}

	private GameObject[] FindTargetArray(string targetTag)
	{
		GameObject[] targetList = GameObject.FindGameObjectsWithTag(targetTag);

		return targetList;
	}
}

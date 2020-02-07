using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class AssignTargetScript : MonoBehaviour
{
	public string targetTag;
	public bool findClosest;
	public bool tauntable;
	public bool magnetizable;

	private Transform target;

	private void Start()
	{
		EventManager.StartListening(ConstantVar.SET_PRIORITY_TARGET, AssignPriorityTarget);
		EventManager.StartListening(ConstantVar.RESET_PRIORITY_TARGET, ResetPriorityTarget);

		EventManager.EmitEvent(ConstantVar.FIND_PRIORITY_TARGET, gameObject);

		AssignTarget();		
	}

	private void Update() {
		if(target != null){
			return;
		}
		AssignTarget();
	}

	private void AssignTarget(){
		if(target != null){
			return;
		}
		if (findClosest)
		{
			target = FindClosestTarget(targetTag, transform.position);
		}
		else
		{
			target = FindTarget(targetTag);
		}
		if(target == null){
			return;
		}
		
		EventManager.SetData(ConstantVar.SET_TARGET, target);
		EventManager.EmitEvent(ConstantVar.SET_TARGET, this.gameObject);
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
			case "taunt":
				if(tauntable){
					target = eventTarget;
				}
			break;
			case "magnet":
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
			case "taunt":
				if(!tauntable){
					return;
				}
			break;
			case "magnet":
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

	private Transform FindTarget(string targetTag)
	{
		GameObject[] targetList = FindTargetArray(targetTag);

		if(targetList.Length > 0)
		{
			return targetList[0].transform;
		}

		return null;
	}

	private Transform FindClosestTarget(string targetTag, Vector3 position)
	{
		GameObject[] targetList = FindTargetArray(targetTag);

		Transform target = null;
		float dist = Mathf.Infinity;

		if (targetList.Length > 0)
		{
			for (int i=0; i<targetList.Length; i++)
			{
				if (target == null)
				{
					target = targetList[i].transform;
				}
				Vector3 directionToTarget = target.position - position;
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

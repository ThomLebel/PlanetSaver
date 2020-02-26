using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using System;

public class AssignTargetScript : MonoBehaviour
{
	public string[] targetTag;
	public bool tauntable;
	public bool magnetizable;

	private Transform target;
	[SerializeField]private string[] originalTarget;

	private void Start()
	{
		originalTarget = new string[targetTag.Length];
		targetTag.CopyTo(originalTarget, 0);

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
		
		target = FindClosestTarget();
		/*if(target == null){
			return;
		}*/
		
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

		targetTag = new string[1]{EventManager.GetString(ConstantVar.MIND_CONTROL)};
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
		
        EventManager.SetData(ConstantVar.USE_ATTACK, false);
        EventManager.EmitEvent(ConstantVar.USE_ATTACK, this.gameObject);
	}

	private void ResetTarget(){
		target = null;
		AssignTarget();
	}

	private Transform FindClosestTarget()
	{
		GameObject[] targetList = FindTargetArray();

		Transform target = null;
		Transform temp = null;
		float dist = Mathf.Infinity;

		if (targetList.Length > 0)
		{
			target = targetList[0].transform;
			dist = (target.position - transform.position).sqrMagnitude;

			for (int i=0; i<targetList.Length; i++)
			{
				temp = targetList[i].transform;

				Vector3 directionToTarget = temp.position - transform.position;
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

	private GameObject[] FindTargetArray()
	{
		GameObject[] list = new GameObject[0];
		for(int i=0; i<targetTag.Length; i++){
			GameObject[] targetList = GameObject.FindGameObjectsWithTag(targetTag[i]);
			Array.Resize(ref list, list.Length + targetList.Length);
			Array.Copy(targetList, 0, list, list.Length - targetList.Length, targetList.Length);
		}

		return list;
	}
}

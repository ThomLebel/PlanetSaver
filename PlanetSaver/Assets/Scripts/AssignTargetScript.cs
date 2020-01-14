using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class AssignTargetScript : MonoBehaviour
{
	public string targetTag;
	public bool findClosest;

	private void Start()
	{
		Transform target = null;
		if (findClosest)
		{
			target = FindClosestTarget(targetTag, transform.position);
		}
		else
		{
			target = FindTarget(targetTag);
		}

		EventManager.SetData("Target", target);
		EventManager.EmitEvent(EventsNames.CommonEvent.SetTarget.ToString(), this.gameObject);
	}

	public Transform FindTarget(string targetTag)
	{
		GameObject[] targetList = FindTargetArray(targetTag);

		if(targetList.Length > 0)
		{
			return targetList[0].transform;
		}

		return null;
	}

	public Transform FindClosestTarget(string targetTag, Vector3 position)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTargetScript : MonoBehaviour
{
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AssignTargetScript))]
public class MoveToTargetScript : MonoBehaviour
{
	public string targetTag;
	public float speed;

	private Transform target;
	private AssignTargetScript assignTarget;

	private void Awake()
	{
		assignTarget = gameObject.GetComponent<AssignTargetScript>();
	}

	private void Start()
	{
		target = assignTarget.FindClosestTarget(targetTag, transform.position);
	}

	// Update is called once per frame
	void Update()
    {
		if (target == null)
		{
			return;
		}
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
	}
}

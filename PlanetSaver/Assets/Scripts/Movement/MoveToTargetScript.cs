using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[RequireComponent(typeof(AssignTargetScript))]
public class MoveToTargetScript : MonoBehaviour
{
	public float speed;

	[SerializeField] private Transform target;
	[SerializeField] private bool destinationReach;

	private void Awake()
	{
		EventManager.StartListening(ConstantVar.DESTINATION_REACH, this.gameObject, DestinationReach);
		EventManager.StartListening(ConstantVar.SET_TARGET, SetTarget);
	}

	// Update is called once per frame
	void Update()
    {
		if (target == null || destinationReach)
		{
			return;
		}
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		transform.up = target.position - transform.position;
	}

	private void SetTarget()
	{
		GameObject sender =(GameObject) EventManager.GetSender(ConstantVar.SET_TARGET);

		if (sender != null && sender == gameObject)
		{
			target = (Transform)EventManager.GetData(ConstantVar.SET_TARGET);
		}
	}

	private void DestinationReach()
	{
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.DESTINATION_REACH);
		if (sender == null || sender != gameObject)
		{
			return;
		}

		destinationReach = EventManager.GetBool(ConstantVar.DESTINATION_REACH);
	}
}

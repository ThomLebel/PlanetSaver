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
		EventManager.StartListening(EventsNames.MovementEvent.DestinationReach.ToString(), DestinationReach);
		EventManager.StartListening(EventsNames.CommonEvent.SetTarget.ToString(), SetTarget);
	}

	private void OnDisable()
	{
		//EventManager.StopListening(EventsNames.MovementEvent.DestinationReach.ToString(), DestinationReach);
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
		var sender = EventManager.GetSender(EventsNames.CommonEvent.SetTarget.ToString());

		if (sender != null)
		{
			GameObject go = (GameObject)sender;

			if (go != gameObject)
			{
				return;
			}

			target = (Transform)EventManager.GetData("Target");
			Debug.Log(gameObject.name+" found a target");
		}
	}

	private void DestinationReach()
	{
		var sender = EventManager.GetSender(EventsNames.MovementEvent.DestinationReach.ToString());
		
		if (sender != null)
		{
			GameObject go = (GameObject)sender;
			Debug.Log(gameObject.name + " received an event from " + go);

			if (go != gameObject)
			{
				return;
			}

			destinationReach = true;
			Debug.Log(gameObject.name + " Destination reached");
		}
	}
}

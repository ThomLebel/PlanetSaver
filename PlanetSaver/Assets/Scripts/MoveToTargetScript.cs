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
		EventManager.StartListening("DestinationReach", DestinationReach);
		EventManager.StartListening("SetTarget", SetTarget);
	}

	private void Start()
	{

	}

	private void OnDisable()
	{
		//EventManager.StopListening("DestinationReach", DestinationReach);
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
		transform.right = target.position - transform.position;
	}

	private void SetTarget()
	{
		var sender = EventManager.GetSender("SetTarget");

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
		var sender = EventManager.GetSender("DestinationReach");
		
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

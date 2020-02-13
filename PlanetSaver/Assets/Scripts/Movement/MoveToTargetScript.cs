using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[RequireComponent(typeof(AssignTargetScript))]
[RequireComponent(typeof(MovementScript))]
public class MoveToTargetScript : MonoBehaviour
{
	public float speed;

	[SerializeField] private Transform target;
	[SerializeField] private bool destinationReach;
	MovementScript movementScript;

	private void Awake()
	{
		movementScript = GetComponent<MovementScript>();
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
		// float step = speed * Time.deltaTime;
		// transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		Vector2 velocity = (target.position - transform.position).normalized;
		// transform.position += velocity * speed * Time.deltaTime;
		transform.up = target.position - transform.position;
		movementScript.Move(velocity);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[RequireComponent(typeof(AssignTargetScript))]
public class OrbitAroundScript : MonoBehaviour
{
	public float orbitDistance;
	public float velocity = 20f;

	private float targetDistance;
	[SerializeField] private bool isOrbiting;
	[SerializeField] private Transform target;

	private void Awake()
	{
		EventManager.StartListening("SetTarget", SetTarget);
	}

	private void Start()
	{

	}

	private void Update()
	{
		Vector3 directionToTarget = target.position - transform.position;
		targetDistance = directionToTarget.sqrMagnitude;

		if (targetDistance <= orbitDistance && !isOrbiting)
		{
			isOrbiting = true;
			EventManager.EmitEvent("DestinationReach", this.gameObject);
			Debug.Log(gameObject.name + " start Orbiting");
		}
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
		}
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		if (!isOrbiting)
		{
			return;
		}

		transform.RotateAround(target.localPosition, Vector3.back, Time.deltaTime*velocity);
		transform.right = target.position - transform.position;
	}
}

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
		EventManager.StartListening(EventsNames.CommonEvent.SetTarget.ToString(), SetTarget);
	}

	private void Update()
    {
        if (target == null)
        {
            return;
        }
        targetDistance = (target.position - transform.position).sqrMagnitude;

        if (targetDistance > orbitDistance || isOrbiting)
        {
            return;
        }
        isOrbiting = true;
        EventManager.EmitEvent(EventsNames.MovementEvent.DestinationReach.ToString(), this.gameObject);
        Debug.Log(gameObject.name + " start Orbiting");
        EventManager.SetData(EventsNames.ActionEvent.UseAttack.ToString(), true);
        EventManager.EmitEvent(EventsNames.ActionEvent.UseAttack.ToString(), "tag:Enemy", 0f, this.gameObject);
    }

    private void SetTarget()
    {
        var sender = EventManager.GetSender(EventsNames.CommonEvent.SetTarget.ToString());

        if (sender == null)
        {
            return;
        }
        GameObject go = (GameObject)sender;

        if (go != gameObject)
        {
            return;
        }

        target = (Transform)EventManager.GetData("Target");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOrbiting)
        {
            transform.RotateAround(target.localPosition, Vector3.back, Time.deltaTime * velocity);
            transform.up = target.position - transform.position;
        }
    }
}

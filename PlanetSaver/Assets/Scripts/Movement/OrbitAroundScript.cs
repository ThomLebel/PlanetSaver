using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[RequireComponent(typeof(AssignTargetScript))]
[RequireComponent(typeof(MovementScript))]
public class OrbitAroundScript : MonoBehaviour
{
	public float orbitDistance;
	public float velocity = 20f;
    [SerializeField] private float distanceOffset = 0.5f;

	private float targetDistance;
	[SerializeField] private bool isOrbiting;
	[SerializeField] private Transform target;

	private void Awake()
	{
		EventManager.StartListening(ConstantVar.SET_TARGET, SetTarget);
	}

	private void Update()
    {
        if (target == null)
        {
            return;
        }
        targetDistance = (target.position - transform.position).sqrMagnitude;

        if(targetDistance <= orbitDistance && !isOrbiting){
            isOrbiting = true;
            EventManager.SetData(ConstantVar.DESTINATION_REACH, isOrbiting);
            EventManager.EmitEvent(ConstantVar.DESTINATION_REACH, this.gameObject);
        }
        if(targetDistance > orbitDistance + distanceOffset && isOrbiting){
            isOrbiting = false;
            EventManager.SetData(ConstantVar.DESTINATION_REACH, isOrbiting);
            EventManager.EmitEvent(ConstantVar.DESTINATION_REACH, this.gameObject);
        }
    }

    private void SetTarget()
    {
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.SET_TARGET);
        if (sender == null || sender != gameObject)
        {
            return;
        }

        target = (Transform)EventManager.GetData(ConstantVar.SET_TARGET);
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

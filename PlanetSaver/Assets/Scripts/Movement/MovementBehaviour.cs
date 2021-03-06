﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[RequireComponent(typeof(AssignTargetScript))]
public class MovementBehaviour : MonoBehaviour
{
    public Behaviour[] movementBehaviours;
    public Behaviour[] steeringBehaviours;
    public float speed;
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 10f)]
    public float neighboorRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    public Vector2 velocity;
    public Vector2 steering;

    [SerializeField] bool canMove = true;
    Vector2 movement;
    
    float moveSpeed;
    float squareMaxSpeed;
    float squareNeighboorsRadius;
    [SerializeField] float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    Collider2D ownCollider;
    [SerializeField] private Transform target;

	private void Awake()
	{
        ownCollider = GetComponent<Collider2D>();
		EventManager.StartListening(ConstantVar.SET_TARGET, SetTarget);
        EventManager.StartListening(ConstantVar.BLOCK_MOVEMENT, BlockMovement);
		EventManager.StartListening(ConstantVar.MIND_CONTROL, MindControl);
		EventManager.StartListening(ConstantVar.RESET_MIND_CONTROL, ResetMindControl);
        EventManager.StartListening(ConstantVar.ADJUST_SPEED, AdjustSpeed);
	}

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = speed;
        squareMaxSpeed = moveSpeed * moveSpeed;
        squareNeighboorsRadius = neighboorRadius * neighboorRadius;
        squareAvoidanceRadius = squareNeighboorsRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!canMove){
            return;
        }

        velocity = CalculateMove(movementBehaviours);
        steering = CalculateMove(steeringBehaviours);
        Vector3 move = velocity + steering;
        move *= driveFactor;
        if(move.sqrMagnitude > squareMaxSpeed){
            move = move.normalized * moveSpeed;
        }
        
        transform.position += move * Time.deltaTime;
        if(target != null){
            transform.up = target.position - transform.position;
        }else{
            transform.up = move;
        }
    }

    private Vector2 CalculateMove(Behaviour[] behaviours){
        if(behaviours.Length <= 0){
            return Vector2.zero;
        }
        
        Vector2 move = Vector2.zero;
        List<Transform> context = GetNearbyObjects();

        //iterate through behaviours
        for(int i=0; i<behaviours.Length; i++){
            EnemyBehaviour behaviour = behaviours[i].behaviour;
            float weight = behaviours[i].weight;

            Vector2 partialMove = Vector2.zero;
            if(weight > 0){
                partialMove = behaviour.CalculateMove(transform, context, target, this) * weight;
            }
            
            if(partialMove != Vector2.zero){
                if(partialMove.sqrMagnitude > weight * weight){
                    partialMove.Normalize();
                    partialMove *= weight;
                }

                move += partialMove;
            }
        }

        return move;
    }

    List<Transform> GetNearbyObjects(){
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, neighboorRadius);
        foreach(Collider2D c in contextColliders){
            if(c.gameObject != gameObject){
                context.Add(c.transform);
            }
        }

        return context;
    }

	private void SetTarget()
	{
		GameObject sender =(GameObject) EventManager.GetSender(ConstantVar.SET_TARGET);
		if (sender != null && sender == gameObject)
		{
			target = (Transform)EventManager.GetData(ConstantVar.SET_TARGET);
		}
	}

    private void BlockMovement(){
        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.BLOCK_MOVEMENT);
        GameObject target = eventData.ToGameObject("target");
        if(target != gameObject){
            return;
        }

        canMove = eventData.ToBool("canMove");
    }

    private void MindControl(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.MIND_CONTROL);
		if(sender == null || sender != gameObject){
			return;
		}

		target = null;
	}

	private void ResetMindControl(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.RESET_MIND_CONTROL);
		if(sender == null || sender != gameObject){
			return;
		}

		target = null;
	}

    private void AdjustSpeed(){
        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.ADJUST_SPEED);

        GameObject target = eventData.ToGameObject("target");
        if(target == null || target != gameObject){
            return;
        }

        float value = eventData.ToFloat("value");
        if(value != 0){
            moveSpeed += Mathf.Floor((moveSpeed * value) / 100);
        }else{
            moveSpeed = speed;
        }
    }

    [Serializable]
    public struct Behaviour{
        public EnemyBehaviour behaviour;
        public float weight;
    }
}

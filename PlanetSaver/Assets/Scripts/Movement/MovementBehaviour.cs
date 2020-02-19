using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[RequireComponent(typeof(AssignTargetScript))]
public class MovementBehaviour : MonoBehaviour
{
    public Behaviour[] behaviours;
    public float speed;
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 10f)]
    public float neighboorRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    public Vector2 velocity;

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
	}

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = speed * speed;
        squareNeighboorsRadius = neighboorRadius * neighboorRadius;
        squareAvoidanceRadius = squareNeighboorsRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = CalculateMove();
        
        transform.position += (Vector3)velocity * Time.deltaTime;
        transform.up = target.position - transform.position;
    }

    private Vector2 CalculateMove(){
        Vector2 move = Vector2.zero;
        List<Transform> context = GetNearbyObjects();

        //iterate through behaviours
        for(int i=0; i<behaviours.Length; i++){
            EnemyBehaviour behaviour = behaviours[i].behaviour;
            float weight = behaviours[i].weight;

            Vector2 partialMove = behaviour.CalculateMove(transform, context, target, this) * weight;
            
            if(partialMove != Vector2.zero){
                if(partialMove.sqrMagnitude > weight * weight){
                    partialMove.Normalize();
                    partialMove *= weight;
                }

                move += partialMove;
            }
        }

        move *= driveFactor;
        if(move.sqrMagnitude > squareMaxSpeed){
            move = move.normalized * speed;
        }

        return move;
    }

    List<Transform> GetNearbyObjects(){
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, neighboorRadius);
        foreach(Collider2D c in contextColliders){
            if(c != ownCollider){
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

    [Serializable]
    public struct Behaviour{
        public EnemyBehaviour behaviour;
        public float weight;
    }
}

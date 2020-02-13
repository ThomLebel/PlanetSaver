using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(MovementScript))]
public class AvoidObstaclesScript : MonoBehaviour
{
    public LayerMask mask;
    [Range(1f, 10f)]
    public float neighboorRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    float squareNeighboorsRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    Collider2D objectCollider;
	MovementScript movementScript;
    
    void Awake(){
		movementScript = GetComponent<MovementScript>();
        objectCollider = GetComponent<Collider2D>();
    }

    private void Start() {
        squareNeighboorsRadius = neighboorRadius * neighboorRadius;
        squareAvoidanceRadius = squareNeighboorsRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        List<Transform> context = GetNearbyObjects();
        AvoidNearbyObstacles(context);
    }

    private void AvoidNearbyObstacles(List<Transform> context){
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;

        foreach(Transform item in context){
            float distance = Vector2.Distance(transform.position, item.position);
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, (item.position - transform.position), distance);

            RaycastHit2D hit = new RaycastHit2D();
            for(int i=0; i<hits.Length; i++){
                hit = hits[i];
                if(hit.transform == item){
                    break;
                }
            }
            
            Vector3 avoidancePoint = (Vector3)hit.point;
            if(Vector2.SqrMagnitude(avoidancePoint - transform.position) < SquareAvoidanceRadius){
                nAvoid ++;
                avoidanceMove += (Vector2)(transform.position - avoidancePoint);
            }
        }
        if(nAvoid > 0){
            avoidanceMove /= nAvoid;
        }

        // transform.up = avoidanceMove;
        // transform.position += (Vector3)avoidanceMove;
        movementScript.Move(avoidanceMove);
    }

    List<Transform> GetNearbyObjects(){
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, neighboorRadius);
        foreach(Collider2D c in contextColliders){
            if(c != objectCollider && mask == (mask | (1 << c.gameObject.layer))){
                context.Add(c.transform);
            }
        }

        return context;
    }
}

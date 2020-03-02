using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/MovementBehaviour/StayInRadius")]
public class StayInRadiusScript : EnemyBehaviour
{
    public Vector2 center;
    public float radius;
    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        Vector2 centerOffset = center - (Vector2)agent.position;
        float t = centerOffset.magnitude / radius;
        if(t < 0.9f){
            return Vector2.zero;
        }
        return centerOffset * t * t;
    }
}

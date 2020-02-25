using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyBehaviour/Behaviour/Follow")]
public class FollowScript : EnemyBehaviour
{
    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        if(target == null){
            return Vector2.zero;
        }

        Vector2 followMove = (target.position - agent.position).normalized;

        return followMove;
    }
}

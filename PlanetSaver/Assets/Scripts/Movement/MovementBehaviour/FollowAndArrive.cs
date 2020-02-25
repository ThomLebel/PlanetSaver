using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[CreateAssetMenu(menuName = "EnemyBehaviour/Behaviour/FollowAndArrive")]
public class FollowAndArrive : EnemyBehaviour
{
    public EnemyBehaviour follow;
    public float stopDistance;
    [Tooltip("distance offset needed to stop orbiting and resume following")]
    public float stopDistanceOffset = 0.5f;

    private bool arrived;

    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        Vector2 move = Vector2.zero;
        if(target == null){
            return move;
        }

        float targetDistance = (target.position - agent.position).sqrMagnitude;

        if(targetDistance > stopDistance + stopDistanceOffset){
            move = follow.CalculateMove(agent, context, target, behaviour);
        }

        if(targetDistance < stopDistance && !arrived){
            arrived = true;
            EventManager.SetData(ConstantVar.DESTINATION_REACH, arrived);
            EventManager.EmitEvent(ConstantVar.DESTINATION_REACH, agent.gameObject);
        }else if(targetDistance > stopDistance + stopDistanceOffset && arrived){
            arrived = false;
            EventManager.SetData(ConstantVar.DESTINATION_REACH, arrived);
            EventManager.EmitEvent(ConstantVar.DESTINATION_REACH, agent.gameObject);
        }

        return move;
    }
}

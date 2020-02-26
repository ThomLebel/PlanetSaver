using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/MovementBehaviour/WanderIfNoTarget")]
public class WanderIfNoTarget : EnemyBehaviour
{
    public EnemyBehaviour wander;
    public EnemyBehaviour behaviourWithTarget;

    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        Vector2 move = Vector2.zero;

        if(target != null){
            move = behaviourWithTarget.CalculateMove(agent, context, target, behaviour);
        }else{
            move = wander.CalculateMove(agent, context, target, behaviour);
        }

        return move;
    }
}

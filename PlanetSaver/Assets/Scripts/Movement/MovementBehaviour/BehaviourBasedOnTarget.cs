using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/MovementBehaviour/BehaviourBasedOnTarget")]
public class BehaviourBasedOnTarget : EnemyBehaviour
{
    public string targetTag;
    [Tooltip("Behaviour to follow when target match")]
    public EnemyBehaviour behaviour1;
    [Tooltip("Behaviour to follow when target doesn't match")]
    public EnemyBehaviour behaviour2;

    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        Vector2 move = Vector2.zero;

        if(target.CompareTag(targetTag)){
            move = behaviour1.CalculateMove(agent, context, target, behaviour);
        }else{
            move = behaviour2.CalculateMove(agent, context, target, behaviour);
        }

        return move;
    }
}

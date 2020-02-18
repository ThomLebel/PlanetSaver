using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyBehaviour/Behaviour/FollowAndOrbit")]
public class FollowAndOrbitScript : EnemyBehaviour
{
    public EnemyBehaviour follow;
    public EnemyBehaviour orbit;
    public float orbitDistance;
    [Tooltip("distance offset needed to stop orbiting and resume following")]
    public float orbitDistanceOffset = 0.5f;

    bool isOrbiting;
    bool repositioning;

    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        Vector2 move = Vector2.zero;
        if(target == null){
            return move;
        }

        float targetDistance = (target.position - agent.position).sqrMagnitude;

        if((targetDistance > orbitDistance && !isOrbiting) || (targetDistance >= orbitDistance + orbitDistanceOffset && isOrbiting)){
            isOrbiting = false;
            move = follow.CalculateMove(agent, context, target, behaviour);
        }
        if((targetDistance <= orbitDistance && targetDistance >= orbitDistance - orbitDistanceOffset) || isOrbiting){
            isOrbiting = true;
            move = orbit.CalculateMove(agent, context, target, behaviour); 
        }
        if(targetDistance < orbitDistance - orbitDistanceOffset && isOrbiting){
            move += (Vector2)(agent.position - target.position).normalized * 0.5f;
        }

        return move;
    }
}

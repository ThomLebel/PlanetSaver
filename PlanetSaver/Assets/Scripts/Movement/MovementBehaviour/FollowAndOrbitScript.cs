using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[CreateAssetMenu(menuName = "IA/MovementBehaviour/FollowAndOrbit")]
public class FollowAndOrbitScript : EnemyBehaviour
{
    public EnemyBehaviour follow;
    public EnemyBehaviour orbit;
    public float orbitDistance;
    [Tooltip("distance offset needed to stop orbiting and resume following")]
    public float orbitDistanceOffset = 0.5f;

    private bool orbiting = false;

    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        Vector2 move = Vector2.zero;
        if(target == null){
            orbiting = false;
            return move;
        }

        float targetDistance = (target.position - agent.position).sqrMagnitude;

        if(targetDistance > orbitDistance + orbitDistanceOffset){
            move = follow.CalculateMove(agent, context, target, behaviour);
        }else if(targetDistance < orbitDistance - orbitDistanceOffset){
            move = (Vector2)(agent.position - target.position).normalized * 0.2f;
        }else{
            move = orbit.CalculateMove(agent, context, target, behaviour);
        }

        if(targetDistance < orbitDistance + orbitDistanceOffset && !orbiting){
            orbiting = true;
            EventManager.SetData(ConstantVar.DESTINATION_REACH, orbiting);
            EventManager.EmitEvent(ConstantVar.DESTINATION_REACH, agent.gameObject);
        }else if(targetDistance > orbitDistance + orbitDistanceOffset && orbiting){
            orbiting = false;
            EventManager.SetData(ConstantVar.DESTINATION_REACH, orbiting);
            EventManager.EmitEvent(ConstantVar.DESTINATION_REACH, agent.gameObject);
        }

        return move;
    }
}

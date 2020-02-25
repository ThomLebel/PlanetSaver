using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyBehaviour/Behaviour/Orbit")]
public class OrbitScript : EnemyBehaviour
{
    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        if(target == null){
            return Vector2.zero;
        }

        Vector3 pos = agent.position;
        Vector3 targetPos = target.localPosition;
        float angle = behaviour.speed * Time.deltaTime;

        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.back); // get the desired rotation
        Vector3 dir = pos - targetPos; // find current direction relative to center
        dir = rot * dir; // rotate the direction
        Vector2 orbitMove = ((targetPos + dir) - pos).normalized; // define new position
        
        return orbitMove;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyBehaviour/Behaviour/Avoidance")]
public class AvoidanceScript : FilteredEnemyBehaviour
{
    public float fieldOfViewAngle = 180f;

    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        //if no neighboors, return no adjustment
        if(context.Count == 0){
            return Vector2.zero;
        }

        //add all points together and average
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach(Transform item in filteredContext){
            float distance = Vector2.Distance(agent.position, item.position);
            Vector2 direction = item.position - agent.position;
            float angle = Vector2.Angle(direction, agent.up);

            Debug.DrawRay(agent.position, direction, Color.green);

            if(angle > fieldOfViewAngle * 0.5f){
                continue;
            }

            RaycastHit2D[] hits = Physics2D.RaycastAll(agent.position, direction, distance);
            Debug.DrawRay(agent.position, direction, Color.red);

            RaycastHit2D hit = new RaycastHit2D();
            for(int i=0; i<hits.Length; i++){
                hit = hits[i];
                if(hit.transform == item){
                    break;
                }
            }
            Vector3 avoidancePoint = (Vector3)hit.point;
            if(Vector2.SqrMagnitude(avoidancePoint - agent.position) < behaviour.SquareAvoidanceRadius){
                nAvoid ++;
                Vector2 avoidDir = (Vector2)(agent.position - avoidancePoint);
                avoidDir = Vector2.Perpendicular(avoidDir);
                if(agent.position.y >= item.position.y){
                    avoidDir.x *= -1;
                    avoidDir.y *= -1;
                }
                avoidanceMove += avoidDir * behaviour.speed;
                Debug.DrawRay(agent.position, avoidanceMove, Color.red);
            }
        }
        // if(nAvoid > 0){
        //     avoidanceMove /= nAvoid;
        // }
        
        return avoidanceMove;
    }
}

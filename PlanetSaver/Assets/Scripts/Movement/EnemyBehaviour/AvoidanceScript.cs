using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyBehaviour/Avoidance")]
public class AvoidanceScript : FilteredEnemyBehaviour
{
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
            float distance = Vector2.Distance(agent.transform.position, item.position);
            RaycastHit2D[] hits = Physics2D.RaycastAll(agent.transform.position, (item.position - agent.transform.position), distance);

            RaycastHit2D hit = new RaycastHit2D();
            for(int i=0; i<hits.Length; i++){
                hit = hits[i];
                if(hit.transform == item){
                    break;
                }
            }
            Vector3 avoidancePoint = (Vector3)hit.point;
            if(Vector2.SqrMagnitude(avoidancePoint - agent.transform.position) < behaviour.SquareAvoidanceRadius){
                nAvoid ++;
                avoidanceMove += (Vector2)(agent.transform.position - avoidancePoint);
            }
        }
        if(nAvoid > 0){
            avoidanceMove /= nAvoid;
        }
        
        return avoidanceMove;
    }
}

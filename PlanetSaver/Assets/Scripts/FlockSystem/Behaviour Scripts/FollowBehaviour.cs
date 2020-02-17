using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Follow")]
public class FollowBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 followMove = Vector2.zero;
        float distance = Mathf.Infinity;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach(Transform target in filteredContext){
           float dist = Vector2.Distance(agent.transform.position, target.position);
           if(dist < distance){
               distance = dist;
               followMove = (target.position - agent.transform.position).normalized;
           }
        }

        return followMove;
    }
}

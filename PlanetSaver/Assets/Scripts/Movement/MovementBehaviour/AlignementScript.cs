using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/MovementBehaviour/Alignement")]
public class AlignementScript : FilteredEnemyBehaviour
{
    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        //if no neighboors, maintain current alignement
        if(context.Count == 0){
            return Vector2.zero;
        }

        //add all points together and average
        Vector2 alignmentMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach(Transform item in filteredContext){
            alignmentMove += (Vector2)item.up;
        }
        alignmentMove /= context.Count;
        
        return alignmentMove;
    }
}

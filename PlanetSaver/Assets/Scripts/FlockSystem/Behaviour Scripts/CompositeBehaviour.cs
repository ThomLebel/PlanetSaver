using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    public Behaviour[] behaviours;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //set up move
        Vector2 move = Vector2.zero;

        //iterate through behaviours
        for(int i =0; i<behaviours.Length; i++){
            FlockBehaviour behaviour = behaviours[i].behaviour;
            float weight = behaviours[i].weight;
            Vector2 partialMove = behaviour.CalculateMove(agent, context, flock) * weight;
            
            if(partialMove != Vector2.zero){
                if(partialMove.sqrMagnitude > weight * weight){
                    partialMove.Normalize();
                    partialMove *= weight;
                }

                move += partialMove;
            }
        }

        return move;
    }

    [Serializable]
    public struct Behaviour{
        public FlockBehaviour behaviour;
        public float weight;
    }
}

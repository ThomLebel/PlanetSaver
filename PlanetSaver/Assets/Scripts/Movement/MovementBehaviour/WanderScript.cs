using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyBehaviour/Behaviour/Wander")]
public class WanderScript : EnemyBehaviour
{
    // public float wanderRadius;
    public TimeLimit timeLimit;

    private float currentTime;
    private Vector2 wanderMove = Vector2.zero;

    public override Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour)
    {
        if(currentTime > 0){
            currentTime -= Time.deltaTime;
            return wanderMove;
        }

        currentTime = UnityEngine.Random.Range(timeLimit.minTime, timeLimit.maxTime);
        wanderMove = UnityEngine.Random.insideUnitCircle.normalized;

        return wanderMove;
    }

    [Serializable]
    public struct TimeLimit{
        public float minTime;
        public float maxTime;
    }
}

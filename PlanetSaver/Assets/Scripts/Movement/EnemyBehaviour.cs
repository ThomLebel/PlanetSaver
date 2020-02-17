using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : ScriptableObject
{
    public abstract Vector2 CalculateMove(Transform agent, List<Transform> context, Transform target, MovementBehaviour behaviour);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaneteAttackScript : ScriptableObject
{
    public string attackName;
    public abstract void Attack();
}

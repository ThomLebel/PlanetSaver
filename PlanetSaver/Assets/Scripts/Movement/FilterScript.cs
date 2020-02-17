using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FilterScript : ScriptableObject
{
    public abstract List<Transform> Filter(Transform agent, List<Transform> original);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Filter/LayerFilter")]
public class LayerFilter : FilterScript
{
    public LayerMask mask;

    public override List<Transform> Filter(Transform agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach(Transform item in original){
            if(mask == (mask | (1 << item.gameObject.layer))){
                filtered.Add(item);
            }
        }

        return filtered;
    }
}

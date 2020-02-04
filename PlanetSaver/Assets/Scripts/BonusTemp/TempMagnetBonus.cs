using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMagnetBonus : TemporaryBonusScript
{
    public float magnetDuration;

    public override void Effect(GameObject player)
    {
        if(player == null){
            return;
        }

        PriorityTargetScript priorityTargetScript = player.GetComponent<PriorityTargetScript>();
        if(priorityTargetScript == null){
            priorityTargetScript = player.AddComponent<PriorityTargetScript>();
        }

        priorityTargetScript.type = "magnet";
        priorityTargetScript.effectDuration = magnetDuration;
    }
}

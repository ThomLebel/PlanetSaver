using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bonus/Magnet")]
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

        priorityTargetScript.type = ConstantVar.BUFF_MAGNET;
        priorityTargetScript.effectDuration = magnetDuration;
    }
}

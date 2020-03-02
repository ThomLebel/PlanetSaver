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

        Magnet priorityTargetScript = player.GetComponent<Magnet>();
        if(priorityTargetScript == null){
            priorityTargetScript = player.AddComponent<Magnet>();
        }

        priorityTargetScript.effectDuration = magnetDuration;
    }
}

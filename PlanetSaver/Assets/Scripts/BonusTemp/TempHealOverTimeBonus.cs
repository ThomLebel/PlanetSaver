using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bonus/HealOverTime")]
public class TempHealOverTimeBonus : TemporaryBonusScript
{
    public float buffDuration = 5f;
    public float healAmount = 2f;

    public override void Effect(GameObject player)
    {
        if(player == null){
            return;
        }

        HealOverTimeScript healOverTimeScript = player.GetComponent<HealOverTimeScript>();
        if(healOverTimeScript == null){
            healOverTimeScript = player.AddComponent<HealOverTimeScript>();
        }

        healOverTimeScript.healAmount = healAmount;
        healOverTimeScript.healDuration = buffDuration;
    }
}

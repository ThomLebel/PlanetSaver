using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDamageReductionBonus : TemporaryBonusScript
{
    public float reductionPercentage = 20f;
    public float reductionDuration = 10f;

    public override void Effect(GameObject player)
    {
        if(player == null){
            return;
        }

        DamageReductionScript damageReductionScript = player.GetComponent<DamageReductionScript>();
        if(damageReductionScript == null){
            damageReductionScript = player.AddComponent<DamageReductionScript>();
        }

        damageReductionScript.reductionDuration = reductionDuration;
        damageReductionScript.damageReduction = reductionPercentage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPoisonBonus : TemporaryBonusScript
{
    public float poisonBuffDuration;
    public float poisonDuration;
    public float poisonDamage;

    public override void Effect(GameObject player)
    {
        if(player == null){
            return;
        }

        PoisonModificatorScript poisonModificator = player.GetComponent<PoisonModificatorScript>();

        if(poisonModificator == null){
            poisonModificator = player.AddComponent<PoisonModificatorScript>();
        }

        poisonModificator.poisonBuffDuration += poisonBuffDuration;
        poisonModificator.poisonDuration = poisonDuration;
        poisonModificator.poisonDamage = poisonDamage;
    }
}

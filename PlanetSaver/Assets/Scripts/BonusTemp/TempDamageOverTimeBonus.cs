using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDamageOverTimeBonus : TemporaryBonusScript
{
    public float dotBuffDuration;
    public float dotDuration;
    public float dotDamage;
    public string attribute;

    public override void Effect(GameObject player)
    {
        if(player == null){
            return;
        }

        DamageOverTimeModificatorScript dotModificator = player.GetComponent<DamageOverTimeModificatorScript>();

        if(dotModificator == null){
            dotModificator = player.AddComponent<DamageOverTimeModificatorScript>();
        }

        dotModificator.dotBuffDuration += dotBuffDuration;
        dotModificator.dotDuration = dotDuration;
        dotModificator.dotDamage = dotDamage;
        dotModificator.attribute = attribute;
    }
}

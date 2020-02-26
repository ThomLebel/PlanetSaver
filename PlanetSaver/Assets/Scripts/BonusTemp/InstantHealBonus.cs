using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[CreateAssetMenu(menuName = "Bonus/InstantHeal")]
public class InstantHealBonus : TemporaryBonusScript
{
    public float minHeal = 10f;
    public float maxHeal = 30f;

    public override void Effect(GameObject player){
        float heal = Random.Range(minHeal, maxHeal);

        EventManager.SetIndexedDataGroup(ConstantVar.HEAL,
            new EventManager.DataGroup{id = "target", data = player},
            new EventManager.DataGroup{id = "value", data = heal}
        );
        EventManager.EmitEvent(ConstantVar.HEAL);
    }
}

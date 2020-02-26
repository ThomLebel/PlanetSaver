using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class HealOnCollision : EffectOnCollision
{
    public float healAmount;
    protected override void Effect()
    {
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.COLLIDE_WITH_SOMETHING);
        if(sender == null || sender != gameObject){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.COLLIDE_WITH_SOMETHING);

        EventManager.SetIndexedDataGroup(ConstantVar.HEAL,
            new EventManager.DataGroup{id = "target", data = target},
            new EventManager.DataGroup{id = "value", data = healAmount}
        );
        EventManager.EmitEvent(ConstantVar.HEAL);
    }
}

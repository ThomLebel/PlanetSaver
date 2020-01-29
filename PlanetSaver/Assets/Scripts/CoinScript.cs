using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class CoinScript : CollectibleScript
{
    public int value;

    protected override void GetCollected(GameObject target){
        if(target == null){
            return;
        }

        EventManager.SetDataGroup(ConstantVar.COLLECT_COIN, target, value);
        EventManager.EmitEvent(ConstantVar.COLLECT_COIN);
        Destroy(this.gameObject);
    }
}

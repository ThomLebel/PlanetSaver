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

        EventManager.SetDataGroup(EventsNames.CommonEvent.CollectCoin.ToString(), target, value);
        EventManager.EmitEvent(EventsNames.CommonEvent.CollectCoin.ToString());
        Destroy(this.gameObject);
    }
}

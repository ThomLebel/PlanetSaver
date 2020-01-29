using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class PlayerScript : MonoBehaviour
{
    public int money;

    private void Start() {
        EventManager.StartListening(ConstantVar.COLLECT_COIN, CollectCoin);
    }

    private void CollectCoin(){
        var eventData = EventManager.GetDataGroup(ConstantVar.COLLECT_COIN);
        GameObject go = eventData[0].ToGameObject();

        if(go != this.gameObject){
            return;
        }

        money += eventData[1].ToInt();
    }
}

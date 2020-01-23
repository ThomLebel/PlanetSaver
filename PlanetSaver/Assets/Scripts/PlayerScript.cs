using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class PlayerScript : MonoBehaviour
{
    public int money;

    private void Start() {
        EventManager.StartListening(EventsNames.CommonEvent.CollectCoin.ToString(), CollectCoin);
    }

    private void CollectCoin(){
        var eventData = EventManager.GetDataGroup(EventsNames.CommonEvent.CollectCoin.ToString());
        GameObject go = eventData[0].ToGameObject();

        if(go != this.gameObject){
            return;
        }

        money += eventData[1].ToInt();
    }
}

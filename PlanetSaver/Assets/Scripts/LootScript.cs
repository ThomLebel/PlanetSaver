using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using System;
using System.Linq;

public class LootScript : MonoBehaviour
{
    [Range(1,99)]
    public int lootChance = 25;
    public int maxObjectLooted = 3;
    [Range(1f,1000f)]
    public float forceModifier = 25;
    public Loot[] loots;

    int totalDropRate;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.DO_DAMAGE, LootObject);

        loots = loots.OrderByDescending(x => x.weight).ToArray();
        foreach(Loot loot in loots){
            totalDropRate += loot.weight;
        }
    }

    private void LootObject(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.DO_DAMAGE);
        if(sender == null){
            return;
        }

        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.DO_DAMAGE);

        AttackInfo attackInfo = (AttackInfo)EventManager.GetData(ConstantVar.DO_DAMAGE);

        if(attackInfo.target != gameObject){
            return;
        }

        int attemptLoot = UnityEngine.Random.Range(0, 100);
        if(attemptLoot > lootChance){
            return;
        }

        int objectLooted = 1;
        if(maxObjectLooted > 1)
            objectLooted = UnityEngine.Random.Range(1, maxObjectLooted);

        for(int i=0; i<objectLooted; i++){
            int randomObjectIndex = UnityEngine.Random.Range(0, totalDropRate);
            
            int lootIndex = 0;
            for(int j=0; j<loots.Length; j++){
                Loot loot = loots[j];
                if(randomObjectIndex <= loot.weight){
                    lootIndex = j;
                    break;
                }else{
                    randomObjectIndex -= loot.weight;
                }
            }

            Vector2 direction = new Vector2(UnityEngine.Random.Range(-forceModifier,forceModifier), UnityEngine.Random.Range(-forceModifier,forceModifier));

            GameObject lootGained = Instantiate(loots[lootIndex].loot, transform.position, Quaternion.identity);
            Rigidbody2D lootRb2d = lootGained.GetComponent<Rigidbody2D>();
            lootRb2d.AddForce(direction * forceModifier);
        }
    }

    [Serializable]
    public struct Loot{
        public GameObject loot;
        public int weight;
    }
}

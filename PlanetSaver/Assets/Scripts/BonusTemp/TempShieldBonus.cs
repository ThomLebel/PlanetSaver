using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShieldBonus : TemporaryBonusScript
{
    public GameObject shieldPrefab;
    public float shieldLifeTime;

    public override void Effect(GameObject player){
        if(player == null){
            return;
        }

        GameObject shield = Instantiate(shieldPrefab, player.transform.position, player.transform.rotation);
        shield.transform.SetParent(player.transform);
        LifeSpanScript lifeSpan = shield.AddComponent<LifeSpanScript>();
        lifeSpan.lifetime = shieldLifeTime;
    }
}

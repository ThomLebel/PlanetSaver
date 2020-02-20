using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bonus/Shield")]
public class TempShieldBonus : TemporaryBonusScript
{
    public GameObject shieldPrefab;
    public float shieldLifeTime;

    public override void Effect(GameObject player){
        if(player == null){
            return;
        }

        GameObject shield = player.transform.Find("shield").gameObject;
        LifeSpanScript lifeSpan = null;
        if(shield == null){
            shield = Instantiate(shieldPrefab, player.transform.position, player.transform.rotation, player.transform);
            lifeSpan = shield.AddComponent<LifeSpanScript>();
        }else{
            lifeSpan = shield.GetComponent<LifeSpanScript>();
        }

        lifeSpan.lifetime += shieldLifeTime;
    }
}

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

        Transform shieldTransform = player.transform.Find("shield");
        GameObject shield = null;
        LifeSpanScript lifeSpan = null;
        if(shieldTransform == null){
            shield = Instantiate(shieldPrefab, player.transform.position, player.transform.rotation, player.transform);
            lifeSpan = shield.AddComponent<LifeSpanScript>();
        }else{
            shield = shieldTransform.gameObject;
            lifeSpan = shield.GetComponent<LifeSpanScript>();
        }

        lifeSpan.lifetime += shieldLifeTime;
    }
}

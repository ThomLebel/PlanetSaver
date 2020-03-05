using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bonus/Shield")]
public class TempShieldBonus : TemporaryBonusScript
{
    public GameObject shieldPrefab;
    public float shieldLifeTime;
    public List<string> blockTarget;

    public override void Effect(GameObject player){
        if(player == null){
            return;
        }

        Transform shieldTransform = player.transform.Find("shield");
        GameObject shield = null;
        LifeSpanScript lifeSpan = null;
        ShieldScript shieldScript = null;
        if(shieldTransform == null){
            shield = Instantiate(shieldPrefab, player.transform.position, player.transform.rotation, player.transform);
            lifeSpan = shield.AddComponent<LifeSpanScript>();
        }else{
            shield = shieldTransform.gameObject;
            lifeSpan = shield.GetComponent<LifeSpanScript>();
        }

        shieldScript = shield.GetComponent<ShieldScript>();
        shieldScript.blockTarget = blockTarget;

        lifeSpan.lifetime += shieldLifeTime;
    }
}

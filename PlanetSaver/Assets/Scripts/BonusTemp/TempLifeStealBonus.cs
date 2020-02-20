using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bonus/LifeSteal")]
public class TempLifeStealBonus : TemporaryBonusScript
{
    public float lifeStealPercentage = 25f;
    public float lifeStealDuration = 5f;

    public override void Effect(GameObject player)
    {
        if(player == null){
            return;
        }

        LifeStealScript LifeStealScript = player.GetComponent<LifeStealScript>();
        if(LifeStealScript == null){
            LifeStealScript = player.AddComponent<LifeStealScript>();
        }

        LifeStealScript.lifeStealPercentage = lifeStealPercentage;
        LifeStealScript.lifeStealDuration = lifeStealDuration;
    }
}

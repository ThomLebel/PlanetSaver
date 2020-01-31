using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DamageReductionScript : DefensiveModifierScript
{
    [Tooltip("Percentage of damage reduction")]
    public float damageReduction = 50f;
    public float reductionDuration;

    void Awake() {
        defensiveModifier.Add(ConstantVar.ATK_ATR_POISON);    
    }

    void Update() {
        if(reductionDuration <= 0){
            Destroy(this);
        }
        reductionDuration -= Time.deltaTime;
    }

    public override void ModifyAttack(AttackInfo attackInfo){
        bool reductible = true;

        for(int i=0; i<attackInfo.attackAttributes.Length; i++){
            string attribute = attackInfo.attackAttributes[i];
            if(defensiveModifier.Contains(attribute)){
                reductible = false;
            }
        }

        if(!reductible){
            return;
        }

        float damageReduced = Mathf.Floor((attackInfo.damage * damageReduction)/100);
        attackInfo.AdjustDamage(damageReduced);
    }
}

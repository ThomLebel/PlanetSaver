﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class InstantHealBonus : TemporaryBonusScript
{
    public float minHeal = 10f;
    public float maxHeal = 30f;

    public override void Effect(GameObject player){
        float heal = Random.Range(minHeal, maxHeal);

        EventManager.SetDataGroup(ConstantVar.HEAL, player, heal);
        EventManager.EmitEvent(ConstantVar.HEAL, "tag:Player");
    }
}

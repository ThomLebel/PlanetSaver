﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class BlockOnCollision : EffectOnCollision
{
    public float blockDuration = 6f;
    
    protected override void Effect()
    {
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.COLLIDE_WITH_SOMETHING);
        if(sender == null || sender != gameObject){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.COLLIDE_WITH_SOMETHING);

        MovementBlockerScript block = target.AddComponent<MovementBlockerScript>();
        block.blockDuration = blockDuration;
    }
}

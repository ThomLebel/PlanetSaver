using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DamageOnCollision : EffectOnCollision
{
    public float collisionDamage;
	public string attributes;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        attributes += ConstantVar.ATK_ATR_COLLISION+",";
    }

    protected override void Effect(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.COLLIDE_WITH_SOMETHING);
        if(sender == null || sender != gameObject){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.COLLIDE_WITH_SOMETHING);

        AttackInfo attackInfo = new AttackInfo(
            initiator,
            gameObject,
            target,
            collisionDamage,
            attributes
        );

        EventManager.SetData(ConstantVar.DO_DAMAGE, attackInfo);
        EventManager.EmitEvent(ConstantVar.DO_DAMAGE, this.gameObject);

        // Destroy(gameObject);
    }
}

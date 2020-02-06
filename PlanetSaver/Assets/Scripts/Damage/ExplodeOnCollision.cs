using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class ExplodeOnCollision : EffectOnCollision
{
    public float explosionDamage;
    public float explosionRadius;
	public string attributes;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        attributes += ConstantVar.ATK_ATR_EXPLOSION+",";
    }
    protected override void Effect()
    {
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.COLLIDE_WITH_SOMETHING);
        if(sender == null || sender != gameObject){
            return;
        }

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        if(targets.Length == 0){
            return;
        }

        //Dégâts dégressif en fonction de la distance d'impact ?
        for(int i = 0; i < targets.Length; i++){
            Collider2D target = targets[i];

            AttackInfo attackInfo = new AttackInfo(
                initiator,
                gameObject,
                target.gameObject,
                explosionDamage,
                attributes
            );

            EventManager.SetData(ConstantVar.DO_DAMAGE, attackInfo);
            EventManager.EmitEvent(ConstantVar.DO_DAMAGE, this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[CreateAssetMenu(menuName = "Skills/Bomb")]
public class BombScript : Skill
{
    public float damage;
    public float radius;
    public string[] targetsTag;

    protected override void Use(GameObject user)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(user.transform.position, radius);
        if(targets.Length == 0){
            return;
        }

        //Dégâts dégressif en fonction de la distance d'impact ?
        for(int i = 0; i < targets.Length; i++){
            Collider2D target = targets[i];
            if(target.gameObject == user){
                continue;
            }
            bool allowed = false;
            foreach(string tag in targetsTag){
                if(target.CompareTag(tag)){
                   allowed = true; 
                }
            }
            if(!allowed){
                continue;
            }

            AttackInfo attackInfo = new AttackInfo(
                user,
                user,
                target.gameObject,
                damage,
                ConstantVar.ATK_ATR_EXPLOSION
            );

            EventManager.SetData(ConstantVar.DO_DAMAGE, attackInfo);
            EventManager.EmitEvent(ConstantVar.DO_DAMAGE, user);
        }
    }
}

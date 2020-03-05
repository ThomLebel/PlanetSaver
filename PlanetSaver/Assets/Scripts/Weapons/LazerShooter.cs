using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class LazerShooter : ShootProjectile
{

    protected override void CreateProjectile(GameObject user){
        GameObject shot = Instantiate(projectile, projectileSpawn.position, user.transform.rotation);

        EventManager.SetDataGroup(ConstantVar.CREATE_BULLET, user, shot);
        EventManager.EmitEvent(ConstantVar.CREATE_BULLET, this.gameObject);

        // shot.tag = owner.tag;
        if(owner.CompareTag("Player") || owner.CompareTag("Friendly")){
            shot.tag = "PlayerBullet";
        }else{
            shot.tag = "EnemyBullet";
        }
        shot.layer = gameObject.layer;
        CollisionScript collisionScript = shot.AddComponent<CollisionScript>();
        collisionScript.targetsTag = targetTags;

        DamageOnCollision damageOnCollision = shot.AddComponent<DamageOnCollision>();
        damageOnCollision.initiator = user;
        damageOnCollision.collisionDamage = damage;
        damageOnCollision.attributes += ConstantVar.ATK_ATR_PIERCING+",";
    }
}

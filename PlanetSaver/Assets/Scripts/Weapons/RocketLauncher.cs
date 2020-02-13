using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class RocketLauncher : ShootProjectile
{

    protected override void CreateProjectile(GameObject user){
        GameObject shot = Instantiate(projectile, projectileSpawn.position, user.transform.rotation);
        shot.transform.up = user.transform.up;
        
        EventManager.SetIndexedDataGroup(ConstantVar.CREATE_BULLET,
            new EventManager.DataGroup{id = "sender", data = user},
            new EventManager.DataGroup{id = "shot", data = shot}
        );
        EventManager.EmitEvent(ConstantVar.CREATE_BULLET, this.gameObject);

        shot.tag = gameObject.tag;
        CollisionScript collisionScript = shot.AddComponent<CollisionScript>();
        collisionScript.targetsTag = targetTags;

        DamageOnCollision damageOnCollision = shot.AddComponent<DamageOnCollision>();
        damageOnCollision.initiator = user;
        damageOnCollision.collisionDamage = damage;
        damageOnCollision.targetsTag = targetTags;
    }
}

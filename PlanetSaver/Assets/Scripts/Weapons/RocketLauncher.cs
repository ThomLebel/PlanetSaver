using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class RocketLauncher : ShootProjectile
{

    protected override void CreateProjectile(GameObject user){
        GameObject shot = Instantiate(projectile, projectileSpawn.position, user.transform.rotation);

        EventManager.SetDataGroup(ConstantVar.CREATE_BULLET, user, shot);
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

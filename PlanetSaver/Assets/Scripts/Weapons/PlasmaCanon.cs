using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class PlasmaCanon : ShootProjectile
{
    public float explosionRadius;

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
        
        ExplodeOnCollision explodeOnCollision = shot.AddComponent<ExplodeOnCollision>();
        explodeOnCollision.initiator = user;
        explodeOnCollision.explosionDamage = damage;
        explodeOnCollision.explosionRadius = explosionRadius;
        explodeOnCollision.targetsTag = targetTags;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaCanon : ShootProjectile
{

    protected override void CreateProjectile(GameObject user){
        GameObject shot = Instantiate(projectile, projectileSpawn.position, user.transform.rotation);
        shot.tag = gameObject.tag;
        CollisionScript collisionScript = shot.AddComponent<CollisionScript>();
        collisionScript.targetsTag = targetTags;
        collisionScript.collisionDamage = damage;
    }
    protected override void BoostWeaponStat()
    {
        throw new System.NotImplementedException();
    }
}

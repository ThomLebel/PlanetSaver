﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerShooter : ShootProjectile
{
    protected override void CreateProjectile(GameObject user){
        GameObject shot = Instantiate(projectile, projectileSpawn.position, user.transform.rotation);
        shot.tag = gameObject.tag;
        CollisionScript collisionScript = shot.AddComponent<CollisionScript>();
        collisionScript.targetsTag = targetTags;
        collisionScript.collisionDamage = damage;
    }
}
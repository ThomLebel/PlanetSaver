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
        collisionScript.initiator = user;
        collisionScript.attributes += ConstantVar.ATK_ATR_EXPLOSION+",";
        collisionScript.targetsTag = targetTags;
        collisionScript.collisionDamage = damage;
    }
    protected override void BoostWeaponStat()
    {
        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.BOOST_WEAPON);

        GameObject target = eventData.ToGameObject("target");
        if(target == null || target != owner){
            return;
        }

        string type = eventData.ToString("type");
        float value = eventData.ToFloat("value");

        switch(type){
            case "damage":
                if(value > 0){
                    damage += Mathf.Floor((damage * value) / 100);
                }else{
                    damage = initialDamage;
                }
            break;
            case "fireRate":
                if(value > 0){
                    fireRate += Mathf.Floor((fireRate * value) / 100);
                }else{
                    fireRate = initialFireRate;
                }
            break;
            default:
            return;
        }
    }
}

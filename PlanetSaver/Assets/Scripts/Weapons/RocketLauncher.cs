using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TigerForge;

public class RocketLauncher : Weapon
{
	public GameObject rocket;
	public Transform rocketSpawn;
	public float fireRate;
	public float damage;

	[SerializeField] private float nextShot = 0f;

	private void Update()
    {
		if(!weaponActive){
			return;
		}

        if (nextShot <= 0f)
        {
            return;
        }
        nextShot -= Time.deltaTime;
    }

    public override void Use(GameObject user)
    {
        base.Use(user);
        if (nextShot > 0f)
        {
            return;
        }
        nextShot += fireRate;

        GameObject shot = Instantiate(rocket, rocketSpawn.position, user.transform.rotation);
        shot.tag = gameObject.tag;
        CollisionScript collisionScript = shot.AddComponent<CollisionScript>();
        collisionScript.targetsTag = targetTags;
        collisionScript.collisionDamage = damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TigerForge;

public class RocketLauncher : Weapon
{
	public GameObject bullet;
	public float fireRate;
	public float damage;

	private float nextShot = 0f;

	private void Start()
	{
		weaponName = "RocketLauncher";
		bullet = WeaponsContainer.instance.munitions.Where(obj => obj.name == "bullet").SingleOrDefault();
		//fireRate = 0.5f;
	}

	private void Update()
	{
		if (nextShot > 0f)
		{
			nextShot -= Time.deltaTime;
		}
	}

	public override void Use(GameObject user)
	{
		base.Use(user);
		if (nextShot <= 0f)
		{
			nextShot += fireRate;
			GameObject shot = Instantiate(bullet, user.transform.position, user.transform.rotation);
			shot.tag = gameObject.tag;
			ImpactScript impact = shot.AddComponent<ImpactScript>();
			impact.targetsTag = targetTags;
			impact.impactDamage = damage;

		}
	}
}

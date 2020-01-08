using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponTest : Weapon
{
	public GameObject bullet;
	public float fireRate;

	private float nextShot = 0f;

	private void Start()
	{
		weaponName = "WeaponTest";
		bullet = WeaponsContainer.instance.munitions.Where(obj => obj.name == "bullet").SingleOrDefault();
		fireRate = 0.5f;
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
			Instantiate(bullet, user.transform.position, user.transform.rotation);
		}
	}
}

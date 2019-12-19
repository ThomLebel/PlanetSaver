using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
	public GameObject bullet;
	public float fireRate;

	private float nextShot = 0f;

	public override void Use()
	{
		base.Use();
		if (nextShot <= 0f)
		{
			nextShot += fireRate;
			Instantiate(bullet, transform.position, transform.rotation);
		}
		if (nextShot > 0f)
		{
			nextShot -= Time.deltaTime;
		}
	}
}

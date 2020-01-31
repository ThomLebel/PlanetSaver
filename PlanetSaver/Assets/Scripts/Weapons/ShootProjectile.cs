using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public abstract class ShootProjectile : Weapon
{
	public GameObject projectile;
	public Transform projectileSpawn;
	public float fireRate;
	public float damage;

    protected float initialDamage;
    protected float initialFireRate;

	[SerializeField] private float nextShot = 0f;

    private void Start() {
        initialDamage = damage;
        initialFireRate = fireRate;

        EventManager.StartListening(ConstantVar.BOOST_WEAPON, BoostWeaponStat);
    }

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
        nextShot = fireRate;
        
        CreateProjectile(user);
    }

    protected abstract void CreateProjectile(GameObject user);
    protected abstract void BoostWeaponStat();
}

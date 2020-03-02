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
    
    protected void BoostWeaponStat()
    {
        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.BOOST_WEAPON);

        GameObject target = eventData.ToGameObject("target");
        if(target == null || target != owner){
            return;
        }

        string type = eventData.ToString("type");
        float value = eventData.ToFloat("value");

        switch(type){
            case ConstantVar.BUFF_DAMAGE:
                if(value != 0){
                    damage += Mathf.Floor((damage * value) / 100);
                }else{
                    damage = initialDamage;
                }
            break;
            case ConstantVar.BUFF_FIRERATE:
                if(value != 0){
                    fireRate -= Mathf.Floor((fireRate * value) / 100);
                }else{
                    fireRate = initialFireRate;
                }
            break;
            default:
            return;
        }
    }
}

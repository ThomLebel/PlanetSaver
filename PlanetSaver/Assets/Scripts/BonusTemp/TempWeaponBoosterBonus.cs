using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bonus/WeaponBooster")]
public class TempWeaponBoosterBonus : TemporaryBonusScript
{
    public string boostType;
    public float boostValue;
    public float boostDuration;

    public override void Effect(GameObject player)
    {
        if(player == null){
            return;
        }

        WeaponBoosterScript weaponBoosterScript = player.GetComponent<WeaponBoosterScript>();
        if(weaponBoosterScript == null){
            weaponBoosterScript = player.AddComponent<WeaponBoosterScript>();
        }

        weaponBoosterScript.boostType = boostType;
        weaponBoosterScript.boostPercentage = boostValue;
        weaponBoosterScript.boostDuration = boostDuration;
        weaponBoosterScript.target = player;
    }
}

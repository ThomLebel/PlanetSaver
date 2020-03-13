using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "General/Shop")]
public class ShopScript : ScriptableObject
{
    public InventaireScript inventaire;
    
    public void Buy(ObjectToBuyScript obt){
        if(obt.price > inventaire.money){
            Debug.Log("You don't have enough money.");
            return;
        }
        switch(obt.type){
            case ObjectType.planet:
                if(!inventaire.planets.Contains(obt.go)){
                    inventaire.planets.Add(obt.go);
                }
            break;
            case ObjectType.ship:
                if(!inventaire.ships.Contains(obt.go)){
                    inventaire.ships.Add(obt.go);
                }
            break;
            case ObjectType.weapon:
                if(!inventaire.weapons.Contains(obt.go)){
                    inventaire.weapons.Add(obt.go);
                }
            break;
        }
    }

    public void Buy(SkillToBuy stb){
        if(stb.price > inventaire.money){
            Debug.Log("You don't have enough money.");
            return;
        }
        if(!inventaire.skills.Contains(stb.skill)){
            inventaire.skills.Add(stb.skill);
        }
    }

    public void Buy(GetBuyableStatsEnum stat){
        switch(stat.statistique){
            case BuyableStats.life:
                inventaire.lives ++;
            break;
            case BuyableStats.weaponSlot:
                if(inventaire.currentWeaponSlot < inventaire.maxWeaponSlot){
                    inventaire.currentWeaponSlot ++;
                }
            break;
            case BuyableStats.skillSlot:
                if(inventaire.currentSkillSlot < inventaire.maxSkillSlot){
                    inventaire.currentSkillSlot ++;
                }
            break;
        }
    }
}

[Serializable]
public enum BuyableStats{
    life,
    weaponSlot,
    skillSlot
}
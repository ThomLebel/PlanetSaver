using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "General/Inventaire")]
public class InventaireScript : ScriptableObject
{
    public GameObject player;
    public List<GameObject> ships;
    public List<GameObject> planets;
    public List<GameObject> weapons;
    public List<Skill> skills;

    public int lives = 1;
    public int money = 0;
    public int maxWeaponSlot = 3;
    public int currentWeaponSlot = 1;
    public int maxSkillSlot = 4;
    public int currentSkillSlot = 0;

    // Start is called before the first frame update
    void Initialize(List<GameObject> _ships = null, List<GameObject> _planets = null, List<GameObject> _weapons = null, List<Skill> _skills = null, int _lives = 1, int _money = 0, int _weaponSlot = 1, int _skillSlot = 0)
    {
        lives = _lives;
        money = _money;
        currentWeaponSlot = _weaponSlot;
        currentSkillSlot = _skillSlot;
        
        if(_ships == null){
            ships = new List<GameObject>();
        }else{
            ships = _ships;
        }
        if(_planets == null){
            planets = new List<GameObject>();
        }else{
            planets = _planets;
        }
        if(_weapons == null){
            weapons = new List<GameObject>();
        }else{
            weapons = _weapons;
        }
        if(_skills == null){
            skills = new List<Skill>();
        }else{
            skills = _skills;
        }
    }
}

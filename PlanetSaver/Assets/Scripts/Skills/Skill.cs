using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName;
    public float cooldown;
    public float timer;
    public GameObject owner;

    public virtual void Initialize(GameObject _owner){
        timer = 0f;
        owner = _owner;
    }

    public void ActivateSkill(GameObject user){
        if(timer > 0f){
            return;
        }
        timer = cooldown;
        
        Use(user);
    }

    protected abstract void Use(GameObject user);
}

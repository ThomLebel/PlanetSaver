using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName;
    public float cooldown;
    public float timer;

    public virtual void Initialize(){
        timer = 0f;
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
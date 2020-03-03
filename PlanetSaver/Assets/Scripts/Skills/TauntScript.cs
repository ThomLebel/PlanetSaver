using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Taunt")]
public class TauntScript : Skill
{
    public float tauntDuration;

    protected override void Use(GameObject user)
    {
        Taunt taunt = user.GetComponent<Taunt>();
        if(taunt == null){
            taunt = user.AddComponent<Taunt>();
        }

        taunt.effectDuration = tauntDuration;
    }
}

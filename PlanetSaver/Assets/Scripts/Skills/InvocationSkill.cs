using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using System;

[CreateAssetMenu(menuName = "Skills/Invocation")]
public class InvocationSkill : Skill
{
    public GameObject invocationPrefab;
    public float lifetime;
    public int maxInvocation;
    [SerializeField]private int currentInvocation;

    public override void Initialize(){
        base.Initialize();

        currentInvocation = 0;
    }
    
    protected override void Use(GameObject user)
    {
        //Can't invoke if there is too much of this type of invocations
        if(currentInvocation >= maxInvocation){
            return;
        }
        currentInvocation ++;

        GameObject invocation = Instantiate(invocationPrefab, user.transform.position, user.transform.rotation);
        Invocation invocInfo = invocation.GetComponent<Invocation>();
        invocInfo.owner = user;
        invocInfo.invocationType = this;
        LifeSpanScript lifespan = invocation.AddComponent<LifeSpanScript>();
        lifespan.lifetime = lifetime;
    }

    public void DeleteInvocation(){
        currentInvocation --;
    }
}

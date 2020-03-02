using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using System;

[CreateAssetMenu(menuName = "Skills/Invocation")]
public class InvocationSkill : Skill
{
    public GameObject invocation;
    public float lifetime;
    public int maxInvocation;
    [SerializeField]private int currentInvocation;
    [SerializeField]private List<Invocation> invocationList;

    public override void Initialize(GameObject _owner){
        base.Initialize(_owner);

        currentInvocation = 0;
        invocationList = new List<Invocation>();
        EventManager.StartListening(ConstantVar.IS_DEAD, InvocationDead);
    }

    // Update is called once per frame
    private void Update()
    {
        if(currentInvocation > 0){
            for(int i=0; i<currentInvocation; i++){
                Invocation invoc = invocationList[i];
                if(invoc.lifetime <= 0f){
                    DeleteInvocation(invoc);
                }
                invoc.lifetime -= Time.deltaTime;
                Debug.Log(invoc.invocation+" lifetime = "+invoc.lifetime);
            }
        }
    }
    
    protected override void Use(GameObject user)
    {
        //Can't invoke if there is too much of this type of invocations
        if(currentInvocation >= maxInvocation){
            return;
        }
        currentInvocation ++;

        GameObject invoc = Instantiate(invocation, user.transform.position, user.transform.rotation);
        invocationList.Add(new Invocation(invoc, lifetime));
    }

    private void InvocationDead(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.IS_DEAD);
        if(sender == null || sender == owner || currentInvocation <= 0){
            return;
        }

        Invocation invoc = new Invocation();

        for(int i = 0; i > invocationList.Count; i++){
            if(invocationList[i].invocation == sender){
                invoc = invocationList[i];
                break;
            }
        }

        DeleteInvocation(invoc);
    }

    private void DeleteInvocation(Invocation invoc){
        invocationList.Remove(invoc);
        currentInvocation --;
    }

    [Serializable]
    public struct Invocation{
        public GameObject invocation;
        public float lifetime;

        public Invocation(GameObject _invocation, float _lifetime){
            invocation = _invocation;
            lifetime = _lifetime;
        }
    }
}

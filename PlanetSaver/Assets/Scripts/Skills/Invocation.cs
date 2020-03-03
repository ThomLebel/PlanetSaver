using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class Invocation : MonoBehaviour
{
    public InvocationSkill invocationType;
    public GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.IS_DEAD, InvocationDead);
    }

    private void InvocationDead(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.IS_DEAD);
        if(sender == null || (sender != gameObject && sender != owner)){
            return;
        }

        invocationType.DeleteInvocation();
    }
}

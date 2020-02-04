using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class PriorityTargetScript : MonoBehaviour
{
    [Tooltip("taunt ou magnet")]
    public string type;
    public float effectDuration;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.FIND_PRIORITY_TARGET, SendPriorityTarget);
        SendPriorityTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(effectDuration <= 0){
            ResetPriorityTarget();
        }
        effectDuration -= Time.deltaTime;
    }

    private void SendPriorityTarget(){
        EventManager.SetIndexedDataGroup(ConstantVar.SET_PRIORITY_TARGET,
            new EventManager.DataGroup{id = "type", data = type},
            new EventManager.DataGroup{id = "target", data = gameObject}
        );
        EventManager.EmitEvent(ConstantVar.SET_PRIORITY_TARGET, gameObject);
    }

    private void ResetPriorityTarget(){
        EventManager.SetData(ConstantVar.RESET_PRIORITY_TARGET, type);
        EventManager.EmitEvent(ConstantVar.RESET_PRIORITY_TARGET, gameObject);

        Destroy(this);
    }
}

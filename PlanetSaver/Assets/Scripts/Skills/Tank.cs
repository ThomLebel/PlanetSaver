using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class Tank : SensorBased
{
    [SerializeField]private List<GameObject> tauntedTargets;

    protected override void Start() {
        base.Start();
        tauntedTargets = new List<GameObject>();
        EventManager.StartListening(ConstantVar.IS_DEAD, ResetPriorityTarget);
    }

    protected override void OnSensorEnter(){
        GameObject sender = (GameObject) EventManager.GetSender(ConstantVar.ON_SENSOR_ENTER);
        if(sender == null || sender != sensor){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.ON_SENSOR_ENTER);
        tauntedTargets.Add(target);
        
        SendPriorityTarget(target);
    }

    private void SendPriorityTarget(GameObject victim){
        EventManager.SetIndexedDataGroup(ConstantVar.SET_PRIORITY_TARGET,
            new EventManager.DataGroup{id = "type", data = ConstantVar.BUFF_TAUNT},
            new EventManager.DataGroup{id = "target", data = gameObject},
            new EventManager.DataGroup{id = "victim", data = victim}
        );
        EventManager.EmitEvent(ConstantVar.SET_PRIORITY_TARGET, gameObject);
    }

    private void ResetPriorityTarget(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.IS_DEAD);
        if(sender == null || sender != gameObject){
            return;
        }

        foreach(GameObject victim in tauntedTargets){
            EventManager.SetIndexedDataGroup(ConstantVar.RESET_PRIORITY_TARGET,
                new EventManager.DataGroup{id = "type", data = ConstantVar.BUFF_TAUNT},
                new EventManager.DataGroup{id = "victim", data = victim}
            );
            EventManager.EmitEvent(ConstantVar.RESET_PRIORITY_TARGET, gameObject);
        }
    }

    protected override void OnSensorExit()
    {
        throw new System.NotImplementedException();
    }
}

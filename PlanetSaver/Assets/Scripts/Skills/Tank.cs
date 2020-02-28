using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class Tank : MonoBehaviour
{
    public SensorScript sensorScript;
    public List<string> targetTag;

    [SerializeField]private GameObject sensor;
    [SerializeField]private List<GameObject> tauntedTargets;

    private void OnValidate() {
        sensorScript = GetComponentInChildren<SensorScript>();
        if(sensorScript != null){
            sensor = sensorScript.gameObject;
        }else{
            Debug.LogError("We need a sensor child attached to "+gameObject);
        }
    }

    private void Start() {
        tauntedTargets = new List<GameObject>();
        sensorScript.targetsTag = targetTag;
        EventManager.StartListening(ConstantVar.ON_SENSOR_ENTER, OnSensorEnter);
        EventManager.StartListening(ConstantVar.IS_DEAD, ResetPriorityTarget);
    }

    private void OnSensorEnter(){
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
}

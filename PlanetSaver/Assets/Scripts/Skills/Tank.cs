using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class Tank : MonoBehaviour
{
    public SensorScript sensorScript;
    public List<string> targetTag;

    [SerializeField]private GameObject sensor;

    private void OnValidate() {
        sensorScript = GetComponentInChildren<SensorScript>();
        if(sensorScript != null){
            sensor = sensorScript.gameObject;
        }else{
            Debug.LogError("We need a sensor child attached to "+gameObject);
        }
    }

    private void Start() {
        sensorScript.targetsTag = targetTag;
        EventManager.StartListening(ConstantVar.ON_SENSOR_ENTER, OnSensorEnter);
    }

    private void OnSensorEnter(){
        GameObject sender = (GameObject) EventManager.GetSender(ConstantVar.ON_SENSOR_ENTER);
        if(sender == null || sender != sensor){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.ON_SENSOR_ENTER);
        
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
}

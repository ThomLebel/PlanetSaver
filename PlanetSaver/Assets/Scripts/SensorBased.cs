using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public abstract class SensorBased : MonoBehaviour
{
    public SensorScript sensorScript;
    public List<string> targetTag;
    [SerializeField]protected GameObject sensor;
    
    private void OnValidate() {
        sensorScript = GetComponentInChildren<SensorScript>();
        if(sensorScript != null){
            sensor = sensorScript.gameObject;
        }else{
            Debug.LogError("We need a sensor child attached to "+gameObject);
        }
    }

    protected virtual void Start()
    {
        sensorScript.targetsTag = targetTag;
        EventManager.StartListening(ConstantVar.ON_SENSOR_ENTER, OnSensorEnter);
        EventManager.StartListening(ConstantVar.ON_SENSOR_EXIT, OnSensorExit);
    }

    protected abstract void OnSensorEnter();
    protected abstract void OnSensorExit();
}

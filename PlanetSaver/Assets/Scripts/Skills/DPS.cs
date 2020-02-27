using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DPS : MonoBehaviour
{
    public SensorScript sensorScript;
    public List<string> targetTag;

    [SerializeField]private GameObject sensor;
    [SerializeField]private GameObject currentTarget;
    [SerializeField]private List<GameObject> targets;
    [SerializeField]private bool targetLocked = false;

    private void OnValidate() {
        sensorScript = GetComponentInChildren<SensorScript>();
        if(sensorScript != null){
            sensor = sensorScript.gameObject;
        }else{
            Debug.LogError("We need a sensor child attached to "+gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targets = new List<GameObject>();
        sensorScript.targetsTag = targetTag;
        EventManager.StartListening(ConstantVar.ON_SENSOR_ENTER, OnSensorEnter);
        EventManager.StartListening(ConstantVar.ON_SENSOR_EXIT, OnSensorExit);
        EventManager.StartListening(ConstantVar.IS_DEAD, TargetISDead);
    }

    private void Update() {
        if(currentTarget != null){
            transform.up = currentTarget.transform.position - transform.position;
        }
    }

    private void OnSensorEnter(){
        GameObject sender = (GameObject) EventManager.GetSender(ConstantVar.ON_SENSOR_ENTER);
        if(sender == null || sender != sensor){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.ON_SENSOR_ENTER);
        targets.Add(target);

        LockTarget();
    }

    private void OnSensorExit(){
        GameObject sender = (GameObject) EventManager.GetSender(ConstantVar.ON_SENSOR_EXIT);
        if(sender == null || sender != sensor){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.ON_SENSOR_EXIT);
        targets.Remove(target);
        
        LockTarget();
    }

    private void TargetISDead(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.IS_DEAD);
		if(sender == null || sender == gameObject || (currentTarget != null && sender != currentTarget)){
			return;
		}

		LockTarget();
    }

    private void LockTarget(){
        if(targets.Count > 0){
            if(targets[0] != currentTarget){
                currentTarget = targets[0];
                targetLocked = true;
                EventManager.SetData(ConstantVar.USE_ATTACK, targetLocked);
                EventManager.EmitEvent(ConstantVar.USE_ATTACK, this.gameObject);
            }
        }else{
            currentTarget = null;
            targetLocked = false;
            EventManager.SetData(ConstantVar.USE_ATTACK, targetLocked);
            EventManager.EmitEvent(ConstantVar.USE_ATTACK, this.gameObject);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DPS : SensorBased
{
    [SerializeField]private GameObject currentTarget;
    [SerializeField]private List<GameObject> targets;
    [SerializeField]private bool targetLocked = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targets = new List<GameObject>();
        EventManager.StartListening(ConstantVar.IS_DEAD, TargetIsDead);
    }

    private void Update() {
        if(currentTarget != null){
            transform.up = currentTarget.transform.position - transform.position;
        }
    }

    protected override void OnSensorEnter(){
        GameObject sender = (GameObject) EventManager.GetSender(ConstantVar.ON_SENSOR_ENTER);
        if(sender == null || sender != sensor){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.ON_SENSOR_ENTER);
        targets.Add(target);

        LockTarget();
    }

    protected override void OnSensorExit(){
        GameObject sender = (GameObject) EventManager.GetSender(ConstantVar.ON_SENSOR_EXIT);
        if(sender == null || sender != sensor){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.ON_SENSOR_EXIT);
        targets.Remove(target);
        
        LockTarget();
    }

    private void TargetIsDead(){
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

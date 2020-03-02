using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class Debuff : SensorBased
{   
    [Range(-100f,0f)] 
    public float debuffValue = -50f;
    [SerializeField]private List<GameObject> targets;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targets = new List<GameObject>();
        EventManager.StartListening(ConstantVar.IS_DEAD, TargetIsDead);
    }

    protected override void OnSensorEnter()
    {
        GameObject sender = (GameObject) EventManager.GetSender(ConstantVar.ON_SENSOR_ENTER);
        if(sender == null || sender != sensor){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.ON_SENSOR_ENTER);
        targets.Add(target);
        DebuffTarget(target, debuffValue);
    }

    protected override void OnSensorExit()
    {
        GameObject sender = (GameObject) EventManager.GetSender(ConstantVar.ON_SENSOR_EXIT);
        if(sender == null || sender != sensor){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.ON_SENSOR_EXIT);
        targets.Remove(target);
        DebuffTarget(target, 0);
    }

    private void TargetIsDead(){
        GameObject sender = (GameObject) EventManager.GetSender(ConstantVar.IS_DEAD);
        if(sender == null || sender != sensor){
            return;
        }

        if(targets.Contains(sender)){
            targets.Remove(sender);
        }
    }

    private void DebuffTarget(GameObject target, float value){
        //Debuff weapon firerate
        EventManager.SetIndexedDataGroup(ConstantVar.BOOST_WEAPON,
            new EventManager.DataGroup{id = "type", data = ConstantVar.BUFF_FIRERATE},
            new EventManager.DataGroup{id = "value", data = value},
            new EventManager.DataGroup{id = "target", data = target}
        );
        EventManager.EmitEvent(ConstantVar.BOOST_WEAPON, gameObject);

        //Debuff movement speed
        EventManager.SetIndexedDataGroup(ConstantVar.ADJUST_SPEED,
            new EventManager.DataGroup{id = "value", data = value},
            new EventManager.DataGroup{id = "target", data = target}
        );
        EventManager.EmitEvent(ConstantVar.ADJUST_SPEED, gameObject);
    }
}

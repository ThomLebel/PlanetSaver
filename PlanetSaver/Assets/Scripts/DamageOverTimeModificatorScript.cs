using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DamageOverTimeModificatorScript : MonoBehaviour
{
    public float dotBuffDuration;
    public float dotDuration;
    public float dotDamage;
    public string attribute;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.CREATE_BULLET, AddEffect);
    }

    void Update(){
        if(dotBuffDuration <= 0){
          EventManager.StopListening(ConstantVar.CREATE_BULLET, AddEffect);
          Destroy(this);
        }
        dotBuffDuration -= Time.deltaTime;
    }

    private void AddEffect(){
        var eventDataGroup = EventManager.GetIndexedDataGroup(ConstantVar.CREATE_BULLET);

        GameObject sender = eventDataGroup.ToGameObject("sender");
        if(sender == null || sender != this.gameObject){
            return;
        }

        GameObject bullet = eventDataGroup.ToGameObject("shot");
        if(bullet == null){
            return;
        }

        DamageOverTimeApplicator dotApplicatorScript = bullet.AddComponent<DamageOverTimeApplicator>();
        dotApplicatorScript.dotDamage = dotDamage;
        dotApplicatorScript.dotDuration = dotDuration;
        dotApplicatorScript.attribute = attribute;
        dotApplicatorScript.initiator = gameObject;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class PoisonModificatorScript : MonoBehaviour
{
    public float poisonBuffDuration;
    public float poisonDuration;
    public float poisonDamage;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.CREATE_BULLET, AddEffect);
    }

    void Update(){
        if(poisonBuffDuration <= 0){
          EventManager.StopListening(ConstantVar.CREATE_BULLET, AddEffect);
          Destroy(this);
        }
        poisonBuffDuration -= Time.deltaTime;
    }

    private void AddEffect(){
        var eventDataGroup = EventManager.GetDataGroup(ConstantVar.CREATE_BULLET);
        if(eventDataGroup == null){
            return;
        }

        GameObject sender = eventDataGroup[0].ToGameObject();
        if(sender == null || sender != this.gameObject){
            return;
        }

        GameObject bullet = eventDataGroup[1].ToGameObject();
        if(bullet == null){
            return;
        }

        PoisonousScript poisonousScript = bullet.AddComponent<PoisonousScript>();
        poisonousScript.poisonDamage = poisonDamage;
        poisonousScript.poisonDuration = poisonDuration;
    }
}

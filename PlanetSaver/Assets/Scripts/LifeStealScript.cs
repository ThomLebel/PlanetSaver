using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class LifeStealScript : MonoBehaviour
{
    [Tooltip("Percentage of life leeched")]
    public float lifeStealPercentage = 25f;
    public float lifeStealDuration;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.TAKE_DAMAGE, LifeSteal);
    }

    void OnDisable() {
        EventManager.StopListening(ConstantVar.TAKE_DAMAGE, LifeSteal);
    }

    void Update() {
        if(lifeStealDuration <= 0f){
            Destroy(this);
        }
        lifeStealDuration -= Time.deltaTime;
    }

    private void LifeSteal()
    {
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.TAKE_DAMAGE);
        if(sender == null || sender == gameObject){
            return;
        }

        AttackInfo attackInfo = (AttackInfo)EventManager.GetData(ConstantVar.TAKE_DAMAGE);
        if (attackInfo.target == this.gameObject || attackInfo.initiator != this.gameObject)
        {
            return;
        }

        float damage = attackInfo.damage;
        float healthLeeched = (damage * lifeStealPercentage) / 100f;

        EventManager.SetIndexedDataGroup(ConstantVar.HEAL,
            new EventManager.DataGroup{id = "target", data = gameObject},
            new EventManager.DataGroup{id = "value", data = healthLeeched}
        );
        EventManager.EmitEvent(ConstantVar.HEAL);
    }
}

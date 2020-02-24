using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class PlaneteAttackManager : MonoBehaviour
{
    public PlaneteAttackScript attack;
    public AttackActivationMode attackActivationMode;

    [Tooltip("Point de vie à perdre avant d'activer l'attaque")]
    public float healthThreshold;

    [Tooltip("Intervalle de temps avant d'activer l'attaque")]
    public float timeThreshold;

    private float healthActivation = 0f;
    private float timeActivation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.TAKE_DAMAGE, DamageTaken);
    }

    // Update is called once per frame
    void Update()
    {
        if(attackActivationMode != AttackActivationMode.Time){
            return;
        }

        timeActivation += Time.deltaTime;
        if(timeActivation >= timeThreshold){
            timeActivation = 0;
            attack.Attack(gameObject);
        }
    }

    void DamageTaken(){
        if(attackActivationMode != AttackActivationMode.Life){
            return;
        }

        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.TAKE_DAMAGE);
        if(sender == null || sender != gameObject){
            return;
        }

        AttackInfo attackInfo = (AttackInfo)EventManager.GetData(ConstantVar.TAKE_DAMAGE);

        healthActivation += attackInfo.damage;
        if(healthActivation >= healthThreshold){
            healthActivation -= healthThreshold;
            attack.Attack(gameObject);
        }
    }

    public enum AttackActivationMode{
        Life,
        Time
    }
}

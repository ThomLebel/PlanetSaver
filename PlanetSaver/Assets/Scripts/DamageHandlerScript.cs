using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DamageHandlerScript : MonoBehaviour
{
    private int attackReceived = 0;
    [SerializeField]private List<DefensiveModifierScript> registeredDefensiveModifier;

    // Start is called before the first frame update
    void Awake() {
        registeredDefensiveModifier = new List<DefensiveModifierScript>();
    }
    void Start()
    {
        EventManager.StartListening(ConstantVar.REGISTER_DEFENSIVE_MODIFIER, this.gameObject, RegisterDefensiveModifier);
        EventManager.StartListening(ConstantVar.DO_DAMAGE, this.gameObject, TakeDamage);
    }

    private void RegisterDefensiveModifier(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.REGISTER_DEFENSIVE_MODIFIER);
        if(sender == null || sender != gameObject){
            return;
        }
        DefensiveModifierScript defensiveModifier = (DefensiveModifierScript)EventManager.GetData(ConstantVar.REGISTER_DEFENSIVE_MODIFIER);
        if(registeredDefensiveModifier.Contains(defensiveModifier)){
            return;
        }
        registeredDefensiveModifier.Add(defensiveModifier);
    }

    private void TakeDamage(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.DO_DAMAGE);
        if(sender == null){
            return;
        }

        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.DO_DAMAGE);

        GameObject target = eventData.ToGameObject("target");
        if(target != gameObject){
            return;
        }

        AttackInfo attackInfo = new AttackInfo(
            attackReceived,
            eventData.ToGameObject("initiator"),
            sender,
            target,
            eventData.ToFloat("damage"),
            eventData.ToString("attributes")
        );

        attackReceived ++;

        foreach(DefensiveModifierScript defensiveModifier in registeredDefensiveModifier){
            defensiveModifier.ModifyAttack(attackInfo);
        }

        //Adjust health based on modified attack
        EventManager.SetData(ConstantVar.TAKE_DAMAGE, attackInfo);
        EventManager.EmitEvent(ConstantVar.TAKE_DAMAGE, gameObject);
    }
}

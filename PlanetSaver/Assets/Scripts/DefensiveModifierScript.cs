using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public abstract class DefensiveModifierScript : MonoBehaviour
{
    public List<string> defensiveModifier;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.SetData(ConstantVar.REGISTER_DEFENSIVE_MODIFIER, this);
        EventManager.EmitEvent(ConstantVar.REGISTER_DEFENSIVE_MODIFIER, this.gameObject);
    }

    public abstract void ModifyAttack(AttackInfo attackInfo);
}

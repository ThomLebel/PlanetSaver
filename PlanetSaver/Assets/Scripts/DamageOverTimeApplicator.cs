using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class DamageOverTimeApplicator : MonoBehaviour
{
    public float dotDuration;
    public float dotDamage;
    public string attribute;
    public GameObject initiator;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.DO_DAMAGE, ApplyPoison);
    }

    private void ApplyPoison(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.DO_DAMAGE);
        if (sender != this.gameObject)
		{
			return;
		}

        AttackInfo attackInfo = (AttackInfo)EventManager.GetData(ConstantVar.DO_DAMAGE);
		if (attackInfo.target == null)
		{
			return;
		}

        DamageOverTimeScript dotScript = attackInfo.target.GetComponent<DamageOverTimeScript>();
        
        if(dotScript == null){
           dotScript = attackInfo.target.AddComponent<DamageOverTimeScript>();
        }
        dotScript.dotDuration = dotDuration;        
        dotScript.dotDamage = dotDamage;
        dotScript.initiator = initiator;    
        dotScript.sender = gameObject;    
    }
}

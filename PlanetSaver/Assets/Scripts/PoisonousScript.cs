using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class PoisonousScript : MonoBehaviour
{
    public float poisonDuration;
    public float poisonDamage;
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

        PoisonScript targetPoisonScript = attackInfo.target.GetComponent<PoisonScript>();
        
        if(targetPoisonScript == null){
           targetPoisonScript = attackInfo.target.AddComponent<PoisonScript>();
        }
        targetPoisonScript.poisonDuration = poisonDuration;        
        targetPoisonScript.poisonDamage = poisonDamage;
        targetPoisonScript.initiator = initiator;    
        targetPoisonScript.sender = gameObject;    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class PoisonousScript : MonoBehaviour
{
    public float poisonDuration;
    public float poisonDamage;
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

        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.DO_DAMAGE);

		GameObject target = eventData.ToGameObject("target");
		if (target == null)
		{
			return;
		}

        PoisonScript targetPoisonScript = target.GetComponent<PoisonScript>();
        
        if(targetPoisonScript == null){
           targetPoisonScript = target.AddComponent<PoisonScript>();
        }
        targetPoisonScript.poisonDuration = poisonDuration;        
        targetPoisonScript.poisonDamage = poisonDamage;        
    }
}

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
        EventManager.StartListening(EventsNames.ActionEvent.DoDamage.ToString(), ApplyPoison);
    }

    private void ApplyPoison(){
        GameObject sender = (GameObject)EventManager.GetSender(EventsNames.ActionEvent.DoDamage.ToString());
        if (sender != this.gameObject)
		{
			return;
		}

        var eventData = EventManager.GetDataGroup(EventsNames.ActionEvent.DoDamage.ToString());

		if (eventData == null)
		{
			return;
		}

		GameObject target = eventData[0].ToGameObject();

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

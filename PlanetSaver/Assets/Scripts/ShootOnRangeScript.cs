using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class ShootOnRangeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		EventManager.StartListening(ConstantVar.DESTINATION_REACH, AttackOnRange);
    }

    private void AttackOnRange(){
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.DESTINATION_REACH);
		if (sender == null || sender != gameObject)
		{
			return;
		}
        
        bool canShoot = EventManager.GetBool(ConstantVar.DESTINATION_REACH);

        EventManager.SetData(ConstantVar.USE_ATTACK, canShoot);
        EventManager.EmitEvent(ConstantVar.USE_ATTACK, this.gameObject);
    }
}

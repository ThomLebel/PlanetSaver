using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class ShootOnRangeScript : MonoBehaviour
{
    [SerializeField] bool canUseWeapon = true;

    // Start is called before the first frame update
    void Start()
    {
		EventManager.StartListening(ConstantVar.DESTINATION_REACH, AttackOnRange);
        EventManager.StartListening(ConstantVar.BLOCK_MOVEMENT, BlockMovement);
    }

    private void AttackOnRange(){
        if(!canUseWeapon){
            return;
        }

        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.DESTINATION_REACH);
		if (sender == null || sender != gameObject)
		{
			return;
		}
        
        bool canShoot = EventManager.GetBool(ConstantVar.DESTINATION_REACH);

        EventManager.SetData(ConstantVar.USE_ATTACK, canShoot);
        EventManager.EmitEvent(ConstantVar.USE_ATTACK, this.gameObject);
    }

    private void BlockMovement(){
        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.BLOCK_MOVEMENT);
        GameObject target = eventData.ToGameObject("target");
        if(target != gameObject){
            return;
        }

        canUseWeapon = eventData.ToBool("canMove");
    }
}

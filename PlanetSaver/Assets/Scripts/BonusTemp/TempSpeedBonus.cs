using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

[CreateAssetMenu(menuName = "Bonus/Speed")]
public class TempSpeedBonus : TemporaryBonusScript
{
    public float speedBuff;
    public float buffDuration;

    public override void Effect(GameObject player)
    {
        if(player == null){
            return;
        }

        EventManager.SetIndexedDataGroup(ConstantVar.BOOST_SPEED,
            new EventManager.DataGroup{id = "target", data = player},
            new EventManager.DataGroup{id = "value", data = speedBuff},
            new EventManager.DataGroup{id = "duration", data = buffDuration}
        );
        EventManager.EmitEvent(ConstantVar.BOOST_SPEED);
    }
}

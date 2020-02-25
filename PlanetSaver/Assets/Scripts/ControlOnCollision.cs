using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class ControlOnCollision : EffectOnCollision
{
    public float effectDuration = 15f;
    [Tooltip("Le nouveau tag que prend la cible contrôlée")]
    public string mindControlTag;
    [Tooltip("La cible que les contrôlés vont attaquer")]
    public string mindControlTarget;

    protected override void Effect()
    {
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.COLLIDE_WITH_SOMETHING);
        if(sender == null || sender != gameObject){
            return;
        }

        GameObject target = EventManager.GetGameObject(ConstantVar.COLLIDE_WITH_SOMETHING);
        if(target.GetComponent<ObstaclesScript>()){
            return;
        }

        MindControlScript mindControl = target.AddComponent<MindControlScript>();
        mindControl.duration = effectDuration;
        mindControl.newTag = mindControlTag;
        mindControl.targetTag = mindControlTarget;
    }
}

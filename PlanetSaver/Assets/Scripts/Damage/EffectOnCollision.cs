using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public abstract class EffectOnCollision : MonoBehaviour
{
	public List<string> targetsTag;
	public GameObject initiator;
    
    void Awake() {
		if(initiator == null){
			initiator = gameObject;
		}
	}

    // Start is called before the first frame update
    protected virtual void Start()
    {
        EventManager.StartListening(ConstantVar.COLLIDE_WITH_SOMETHING, Effect);
    }

    protected abstract void Effect();
}

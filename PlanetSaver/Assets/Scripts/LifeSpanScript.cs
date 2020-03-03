using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class LifeSpanScript : MonoBehaviour
{
	public float lifetime;

    // Update is called once per frame
    void Update()
    {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0f)
		{
			Destroy(gameObject);
			EventManager.EmitEvent(ConstantVar.IS_DEAD, gameObject);
		}
    }
}

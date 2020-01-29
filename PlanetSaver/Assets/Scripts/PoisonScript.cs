using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class PoisonScript : MonoBehaviour
{
    public float poisonDamage;
    public float poisonDuration;
    public float poisonTick = .5f;

    private IEnumerator poisonCoroutine;
    private WaitForSeconds wait;


    // Start is called before the first frame update
    void Start()
    {
        wait = new WaitForSeconds(poisonTick);
        poisonCoroutine = PoisonHit();
        StartCoroutine(poisonCoroutine);
    }

    void Update(){
        if(poisonDuration <= 0f){
            StopCoroutine(poisonCoroutine);
            Destroy(this);
        }
        poisonDuration -= Time.deltaTime;
    }

    public void ExtendPoisonDuration(float duration){
        if(poisonDuration > 0f && duration > 0){
            poisonDuration += duration;
        }
    }

    private IEnumerator PoisonHit(){
        while(true){
            yield return wait;

            EventManager.SetIndexedDataGroup(ConstantVar.DO_DAMAGE,
					new EventManager.DataGroup{id = "target", data = this.gameObject},
					new EventManager.DataGroup{id = "damage", data = poisonDamage}
			);
            EventManager.EmitEvent(ConstantVar.DO_DAMAGE, this.gameObject);
        }
        
    }
}

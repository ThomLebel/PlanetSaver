using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class DamageOverTimeScript : MonoBehaviour
{
    public GameObject initiator;
    public GameObject sender;
    public float dotDamage;
    public float dotDuration;
    public float dotTick = .5f;
    public string attribute;

    private IEnumerator poisonCoroutine;
    private WaitForSeconds wait;

    // Start is called before the first frame update
    void Start()
    {
        wait = new WaitForSeconds(dotTick);
        poisonCoroutine = PoisonHit();
        StartCoroutine(poisonCoroutine);
    }

    void Update(){
        if(dotDuration <= 0f){
            StopCoroutine(poisonCoroutine);
            Destroy(this);
        }
        dotDuration -= Time.deltaTime;
    }

    public void ExtendPoisonDuration(float duration){
        if(dotDuration > 0f && duration > 0){
            dotDuration += duration;
        }
    }

    private IEnumerator PoisonHit(){
        while(true){
            yield return wait;

            AttackInfo attackInfo = new AttackInfo(
                initiator,
                sender,
                gameObject,
                dotDamage,
                attribute
            );

            EventManager.SetData(ConstantVar.DO_DAMAGE, attackInfo);
            EventManager.EmitEvent(ConstantVar.DO_DAMAGE, this.gameObject);
        }
        
    }
}
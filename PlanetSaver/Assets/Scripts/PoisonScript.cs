using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class PoisonScript : MonoBehaviour
{
    public GameObject initiator;
    public GameObject sender;
    public float poisonDamage;
    public float poisonDuration;
    public float poisonTick = .5f;

    [SerializeField]private string attribute;
    private IEnumerator poisonCoroutine;
    private WaitForSeconds wait;

    void Awake() {
        attribute = ConstantVar.ATK_ATR_POISON;    
    }

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

            AttackInfo attackInfo = new AttackInfo(
                initiator,
                sender,
                gameObject,
                poisonDamage,
                attribute
            );

            EventManager.SetData(ConstantVar.DO_DAMAGE, attackInfo);
            EventManager.EmitEvent(ConstantVar.DO_DAMAGE, this.gameObject);
        }
        
    }
}
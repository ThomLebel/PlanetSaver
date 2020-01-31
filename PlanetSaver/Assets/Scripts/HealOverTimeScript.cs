using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class HealOverTimeScript : MonoBehaviour
{
    public float healAmount;
    public float healDuration;
    public float healTick = 0.5f;

    private IEnumerator healOverTimeCoroutine;
    private WaitForSeconds wait;


    // Start is called before the first frame update
    void Start()
    {
        wait = new WaitForSeconds(healTick);
        healOverTimeCoroutine = HealOverTime();
        StartCoroutine(healOverTimeCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if(healDuration <= 0){
            StopCoroutine(healOverTimeCoroutine);
            Destroy(this);
        }
        healDuration -= Time.deltaTime;
    }

    private IEnumerator HealOverTime(){
        while(true){
            yield return wait;

            EventManager.SetDataGroup(ConstantVar.HEAL, gameObject, healAmount);
            EventManager.EmitEvent(ConstantVar.HEAL, "tag:Player");
        }
    }
}

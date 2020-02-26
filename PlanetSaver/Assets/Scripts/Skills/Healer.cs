using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public GameObject healPrefab;
    public float healAmount;
    public float healInterval;

    private WaitForSeconds wait;
    private IEnumerator healCoroutine;
    private Animator healAnimator;
    private GameObject healCircle;

    // Start is called before the first frame update
    void Start()
    {
        healCircle = Instantiate(healPrefab, transform.position, Quaternion.identity);
        healCircle.GetComponent<HealOnCollision>().healAmount = healAmount;
        healAnimator = healCircle.GetComponent<Animator>();
        wait = new WaitForSeconds(healInterval);
        healCoroutine = Heal();
        StartCoroutine(healCoroutine);
    }
    
    private void OnDestroy() {
        StopCoroutine(healCoroutine);
        Destroy(healCircle);
    }

    private IEnumerator Heal(){
        while(true){
            healAnimator.SetTrigger("Heal");
            
            yield return wait;
        }
    }
}

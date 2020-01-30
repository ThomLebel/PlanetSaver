using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class LeechHealthScript : MonoBehaviour
{
    [Tooltip("Percentage of life leeched")]
    public float leechPercentage = 25f;
    public float leechDuration;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.TAKE_DAMAGE, LeechHealth);
    }

    void OnDisable() {
        EventManager.StopListening(ConstantVar.TAKE_DAMAGE, LeechHealth);
    }

    void Update() {
        if(leechDuration <= 0f){
            Destroy(this);
        }
        leechDuration -= Time.deltaTime;
    }

    private void LeechHealth()
    {
        GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.TAKE_DAMAGE);
        if(sender == null || sender == gameObject){
            return;
        }

        AttackInfo attackInfo = (AttackInfo)EventManager.GetData(ConstantVar.TAKE_DAMAGE);
        if (attackInfo.target == this.gameObject || attackInfo.initiator != this.gameObject)
        {
            return;
        }

        float damage = attackInfo.damage;
        float healthLeeched = (damage * leechPercentage) / 100f;

        EventManager.SetDataGroup(ConstantVar.HEAL, gameObject, healthLeeched);
        EventManager.EmitEvent(ConstantVar.HEAL, this.gameObject);
        Debug.Log("we heal the player");
    }
}

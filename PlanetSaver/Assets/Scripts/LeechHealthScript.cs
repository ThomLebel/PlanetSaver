using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class LeechHealthScript : MonoBehaviour
{
    public float leechPercentage;
    public float leechDuration;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(ConstantVar.DO_DAMAGE, LeechHealth);
    }

    private void LeechHealth()
    {
        var eventData = EventManager.GetIndexedDataGroup(ConstantVar.DO_DAMAGE);

        GameObject target = eventData.ToGameObject("target");
        if (target != this.gameObject)
        {
            return;
        }


        //Sauf que le sender est la balle et non le personnage :/
        GameObject healTarget = (GameObject)EventManager.GetSender(ConstantVar.DO_DAMAGE);

        if (healTarget == null || healTarget != gameObject)
        {
            return;
        }

        float damage = eventData.ToFloat("damage");
        float healthLeeched = (damage * leechPercentage) / 100f;

        EventManager.SetDataGroup(ConstantVar.HEAL, healTarget, healthLeeched);
        EventManager.EmitEvent(ConstantVar.HEAL, this.gameObject);
    }
}

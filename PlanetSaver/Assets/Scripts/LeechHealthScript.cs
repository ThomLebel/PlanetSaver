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
        EventManager.StartListening(EventsNames.ActionEvent.DoDamage.ToString(), LeechHealth);
    }

    private void LeechHealth()
    {
        var eventData = EventManager.GetDataGroup(EventsNames.ActionEvent.DoDamage.ToString());

        if (eventData == null)
        {
            return;
        }

        GameObject target = eventData[0].ToGameObject();

        if (target != this.gameObject)
        {
            return;
        }


        //Sauf que le sender est la balle et non le personnage :/
        var sender = EventManager.GetSender(EventsNames.ActionEvent.DoDamage.ToString());
        if (sender == null)
        {
            return;
        }
        GameObject healTarget = (GameObject)sender;

        if (healTarget != gameObject)
        {
            return;
        }

        float damage = eventData[1].ToFloat();
        float healthLeeched = (damage * leechPercentage) / 100f;

        EventManager.SetDataGroup(EventsNames.ActionEvent.Heal.ToString(), healTarget, healthLeeched);
        EventManager.EmitEvent(EventsNames.ActionEvent.Heal.ToString(), this.gameObject);
    }
}

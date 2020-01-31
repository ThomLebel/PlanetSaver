using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class WeaponBoosterScript : MonoBehaviour
{
    public string boostType;
    public float boostPercentage;
    public float boostDuration;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.SetIndexedDataGroup(ConstantVar.BOOST_WEAPON,
            new EventManager.DataGroup{id = "type", data = boostType},
            new EventManager.DataGroup{id = "value", data = boostPercentage},
            new EventManager.DataGroup{id = "target", data = target}
        );
        EventManager.EmitEvent(ConstantVar.BOOST_WEAPON, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(boostDuration <= 0){
            EventManager.SetIndexedDataGroup(ConstantVar.BOOST_WEAPON,
                new EventManager.DataGroup{id = "type", data = boostType},
                new EventManager.DataGroup{id = "value", data = 0},
                new EventManager.DataGroup{id = "target", data = target}
            );
            EventManager.EmitEvent(ConstantVar.BOOST_WEAPON, gameObject);
            Destroy(this);
        }
        boostDuration -= Time.deltaTime;
    }
}

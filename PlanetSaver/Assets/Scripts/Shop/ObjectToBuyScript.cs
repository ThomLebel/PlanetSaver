using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "General/ObjectToBuy")]
public class ObjectToBuyScript : ScriptableObject
{
    public GameObject go;
    public ObjectType type;
    public int price;
}

[Serializable]
public enum ObjectType{
    ship,
    planet,
    weapon,
    skill
}

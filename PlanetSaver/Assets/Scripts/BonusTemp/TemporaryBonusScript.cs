using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TemporaryBonusScript : MonoBehaviour
{
    public string bonusName;
    
    public abstract void Effect(GameObject player);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : PriorityTargetScript
{
    private void Awake() {
        type = ConstantVar.BUFF_TAUNT;
    }
}

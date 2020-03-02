using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : PriorityTargetScript
{
    private void Awake() {
        type = ConstantVar.BUFF_MAGNET;
    }
}

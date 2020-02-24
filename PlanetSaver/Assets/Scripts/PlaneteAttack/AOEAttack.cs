using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlaneteAttack/AreaOfEffect")]
public class AOEAttack : PlaneteAttackScript
{
    public GameObject aoe;

    public override void Attack(GameObject planete)
    {
        GameObject attack = Instantiate(aoe, planete.transform.position, Quaternion.identity);
    }
}

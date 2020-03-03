using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Teleportation")]
public class TeleportationScript : Skill
{
    public float teleportationDistance;
    protected override void Use(GameObject user)
    {
        user.transform.position += user.transform.up * teleportationDistance;
    }
}

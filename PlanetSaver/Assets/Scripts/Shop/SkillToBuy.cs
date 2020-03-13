using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "General/SkillToBuy")]
public class SkillToBuy : ScriptableObject
{
    public Skill skill;
    public int price;
}

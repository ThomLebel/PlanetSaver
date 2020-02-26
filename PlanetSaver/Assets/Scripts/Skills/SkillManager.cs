using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class SkillManager : MonoBehaviour
{
	public List<Skill> skillList;

	private void Awake()
	{
		EventManager.StartListening(ConstantVar.USE_SKILL, UseSkill);
	}

	private void Start() {
		foreach(Skill skill in skillList){
			skill.Initialize(gameObject);
		}
	}

	private void Update() {
		for(int i=0; i<skillList.Count; i++){
			Skill skill = skillList[i];
			if(skill.timer <= 0f){
				skill.timer = 0f;
				continue;
			}
			skill.timer -= Time.deltaTime;
		}
	}

	private void UseSkill()
	{
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.USE_SKILL);
		if(sender == null || sender != gameObject){
			return;
		}

		int skillID = EventManager.GetInt(ConstantVar.USE_SKILL);

		if(skillList.Count <= skillID){
			return;
		}

		skillList[skillID].ActivateSkill(gameObject);


		//Global cooldown on all other skills ?
	}
}

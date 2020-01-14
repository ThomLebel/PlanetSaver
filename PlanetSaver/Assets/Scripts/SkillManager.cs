using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class SkillManager : MonoBehaviour
{
	public List<Weapon> skillList;

	public bool skill1;
	public bool skill2;
	public bool skill3;
	public bool skill4;

	private void Awake()
	{
		EventManager.StartListening(EventsNames.ActionEvent.UseSkill.ToString(), this.gameObject, UseSkill);
	}

	// Update is called once per frame
	void Update()
	{
		if (skill1)
		{
			skillList[0].Use(gameObject);
		}
		if (skill2)
		{
			skillList[1].Use(gameObject);
		}
		if (skill3)
		{
			skillList[2].Use(gameObject);
		}
		if (skill4)
		{
			skillList[3].Use(gameObject);
		}
	}

	private void UseSkill()
	{
		var sender = EventManager.GetSender(EventsNames.ActionEvent.UseSkill.ToString());

		if (sender != null)
		{
			GameObject go = (GameObject)sender;

			if (go != gameObject)
			{
				return;
			}

			var eventData = EventManager.GetDataGroup(EventsNames.ActionEvent.UseAttack.ToString());
			string skill = eventData[0].ToString();
			bool skillOn = eventData[1].ToBool();

			switch (skill)
			{
				case "Skill1":
					skill1 = skillOn;
					break;
				case "Skill2":
					skill2 = skillOn;
					break;
				case "Skill3":
					skill3 = skillOn;
					break;
				case "Skill4":
					skill4 = skillOn;
					break;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class OwnerScript : MonoBehaviour
{
	public GameObject owner;

	private void Awake()
	{
		EventManager.StartListening(EventsNames.CommonEvent.GetOwner.ToString(), GetOwner);
	}

	private void GetOwner()
	{
		var sender = EventManager.GetSender(EventsNames.CommonEvent.GetOwner.ToString());

		if (sender != null)
		{
			GameObject go = (GameObject)sender;

			if (go != gameObject)
			{
				return;
			}

			//Do Stuff
		}
	}
}

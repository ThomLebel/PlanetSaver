using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class OwnerScript : MonoBehaviour
{
	public GameObject owner;

	private void Awake()
	{
		EventManager.StartListening(ConstantVar.GET_OWNER, GetOwner);
	}

	private void GetOwner()
	{
		var sender = EventManager.GetSender(ConstantVar.GET_OWNER);

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

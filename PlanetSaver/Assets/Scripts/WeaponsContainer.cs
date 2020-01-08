using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsContainer : MonoBehaviour
{
	public List<GameObject> munitions;

	public static WeaponsContainer instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}
}

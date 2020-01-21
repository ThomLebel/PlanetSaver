using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class GameManagerScript : MonoBehaviour
{
	public int enemyLimit = 5;

	[SerializeField] private int currentEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
		EventManager.StartListening(EventsNames.CommonEvent.EnemySpawned.ToString(), EnemySpawned);
    }

	private void OnDisable()
	{
		EventManager.StopListening(EventsNames.CommonEvent.EnemySpawned.ToString(), EnemySpawned);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void EnemySpawned()
	{
		currentEnemyCount++;

		if (currentEnemyCount >= enemyLimit)
		{
			EventManager.EmitEvent(EventsNames.CommonEvent.StopSpawning.ToString());
			currentEnemyCount = 0;
		}
	}
}

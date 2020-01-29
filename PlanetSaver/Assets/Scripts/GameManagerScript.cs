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
		EventManager.StartListening(ConstantVar.ENEMY_SPAWNED, EnemySpawned);
    }

	private void OnDisable()
	{
		EventManager.StopListening(ConstantVar.ENEMY_SPAWNED, EnemySpawned);
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
			EventManager.EmitEvent(ConstantVar.STOP_SPAWNING);
			currentEnemyCount = 0;
		}
	}
}

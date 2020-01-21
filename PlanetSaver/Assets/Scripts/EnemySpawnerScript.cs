using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class EnemySpawnerScript : MonoBehaviour
{
	public List<GameObject> enemies;
	public float spawnRate;
	public float offset;

	public float angleInterval = 45f;
	[SerializeField] private float minAngle;
	[SerializeField] private float maxAngle;

	[SerializeField] private float circleRadius;
	private float nextSpawn = 0f;
	private Camera cam;
	private Transform cameraRig;
	private bool stopSpawning;

	private void Awake()
	{
		cam = Camera.main;
		cameraRig = cam.GetComponentInParent<Transform>();
		minAngle = Random.Range(0, 360);
		maxAngle = minAngle + angleInterval;
	}

	private void Start()
	{
		EventManager.StartListening(EventsNames.CommonEvent.StopSpawning.ToString(), StopSpawning);
	}

	private void OnDisable()
	{
		EventManager.StopListening(EventsNames.CommonEvent.StopSpawning.ToString(), StopSpawning);
	}

	// Update is called once per frame
	void Update()
    {
		if (stopSpawning)
			return;

		if (nextSpawn > 0f)
		{
			nextSpawn -= Time.deltaTime;
		}
		else
		{
			SpawnEnemy();
			nextSpawn = spawnRate;
		}
	}

	private void SpawnEnemy()
	{
		int enemyIndex = Random.Range(0, enemies.Count);
		Vector3 enemyPos = GetRandomPointOnSpawnCircle();
		GameObject enemy = Instantiate(enemies[enemyIndex], enemyPos, Quaternion.identity);
		EventManager.EmitEvent(EventsNames.CommonEvent.EnemySpawned.ToString());
	}

	private void StopSpawning()
	{
		stopSpawning = true;
	}

	private Vector3 GetRandomPointOnSpawnCircle()
	{
		Vector3 pos;

		circleRadius = (cam.orthographicSize * cam.aspect) + Mathf.Abs(cameraRig.position.x) + offset;

		float degree = Random.Range(minAngle, maxAngle);
		float radian = degree * Mathf.Deg2Rad;
		float x = Mathf.Cos(radian);
		float y = Mathf.Sin(radian);
		pos = new Vector3(x, y, 0) * circleRadius;

		return pos;
	}
}

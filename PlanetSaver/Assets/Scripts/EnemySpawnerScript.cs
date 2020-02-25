using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using System;

public class EnemySpawnerScript : MonoBehaviour
{
	public List<GameObject> enemies;
	public float spawnRate;
	public float offset;

	public float angleInterval = 45f;
	[SerializeField] private SpawnArea spawnArea;

	[SerializeField] private float circleRadius;
	private float nextSpawn = 0f;
	private Camera cam;
	private Transform cameraRig;
	private bool stopSpawning;
	[SerializeField] private List<SpawnArea> spawnAreas;
	[SerializeField] private int activeSpawnAreas = 1;

	private void Awake()
	{
		cam = Camera.main;
		cameraRig = cam.GetComponentInParent<Transform>();
		spawnArea.minAngle = UnityEngine.Random.Range(0, 360);
		spawnArea.maxAngle = spawnArea.minAngle + angleInterval;

		spawnAreas = new List<SpawnArea>();

		for(int i = 0; i < 8; i++){
			spawnAreas.Add(new SpawnArea(spawnArea.minAngle, spawnArea.maxAngle));
			spawnArea.minAngle = spawnArea.maxAngle;
			spawnArea.maxAngle += angleInterval;
		}

		spawnAreas = Helpers.Shuffle<SpawnArea>(spawnAreas);
	}

	private void Start()
	{
		EventManager.StartListening(ConstantVar.STOP_SPAWNING, StopSpawning);
	}

	private void OnDisable()
	{
		EventManager.StopListening(ConstantVar.STOP_SPAWNING, StopSpawning);
	}

	// Update is called once per frame
	void Update()
    {
		if (stopSpawning)
			return;

        if (nextSpawn <= 0f)
        {
            SpawnEnemy();
            nextSpawn = spawnRate;
        }
        else
        {
            nextSpawn -= Time.deltaTime;
        }
    }

	private void SpawnEnemy()
	{
		for(int i=0; i<activeSpawnAreas; i++){
			int enemyIndex = UnityEngine.Random.Range(0, enemies.Count);
			Vector3 enemyPos = GetRandomPointOnSpawnCircle(i);
			GameObject enemy = Instantiate(enemies[enemyIndex], enemyPos, Quaternion.identity);
			EventManager.EmitEvent(ConstantVar.ENEMY_SPAWNED);
		}
	}

	private void StopSpawning()
	{
		stopSpawning = true;
	}

	private void AddSpawnArea(){
		activeSpawnAreas ++;
	}

	private Vector3 GetRandomPointOnSpawnCircle(int index)
	{
        circleRadius = (cam.orthographicSize * cam.aspect) + Mathf.Abs(cameraRig.position.x) + offset;
		
		float degree = UnityEngine.Random.Range(spawnAreas[index].minAngle, spawnAreas[index].maxAngle);
		float radian = degree * Mathf.Deg2Rad;
		float x = Mathf.Cos(radian);
		float y = Mathf.Sin(radian);
		
        Vector3 pos = new Vector3(x, y, 0) * circleRadius;

        return pos;
	}

	[Serializable]
	public struct SpawnArea{
		public float minAngle;
		public float maxAngle;

		public SpawnArea(float _minAngle, float _maxAngle){
			minAngle = _minAngle;
			maxAngle = _maxAngle;
		}
	}
}

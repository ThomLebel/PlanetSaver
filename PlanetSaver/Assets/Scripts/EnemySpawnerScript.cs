using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using System;

public class EnemySpawnerScript : MonoBehaviour
{
	public List<GameObject> enemies;
	public float spawnRate;
	public float expandTimer = 60f;
	public float offset;
	public float angleInterval = 45f;
	[SerializeField] private SpawnArea spawnArea;

	[SerializeField] private float circleRadius;
	private Camera cam;
	private Transform cameraRig;
	private bool stopSpawning;
	[SerializeField] private List<SpawnArea> spawnAreas;
	[SerializeField] private int activeSpawnAreas = 1;
	private WaitForSeconds wait;
	private IEnumerator expandSpawnArea;

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

		wait = new WaitForSeconds(expandTimer);
		expandSpawnArea = ExpandSpawnArea();
		StartCoroutine(expandSpawnArea);
	}

	private void Start()
	{
		EventManager.StartListening(ConstantVar.STOP_SPAWNING, StopSpawning);
		EventManager.StartListening(ConstantVar.IS_DEAD, EnemyDead);
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

		for(int i=0; i<activeSpawnAreas; i++){
			SpawnArea spawn = spawnAreas[i];
			if(spawnArea.nextSpawn <= 0f){
				SpawnEnemy(spawn);
				spawnArea.nextSpawn = spawnRate;
			}else{
				spawnArea.nextSpawn -= Time.deltaTime;
			}
		}
    }

	private void SpawnEnemy(SpawnArea spawn)
	{
		int enemyIndex = UnityEngine.Random.Range(0, enemies.Count);
		Vector3 enemyPos = GetRandomPointOnSpawnCircle(spawn);
		GameObject enemy = Instantiate(enemies[enemyIndex], enemyPos, Quaternion.identity);
		spawn.enemies.Add(enemy);
		EventManager.EmitEvent(ConstantVar.ENEMY_SPAWNED);
	}

	private void StopSpawning()
	{
		stopSpawning = true;
	}

	private void AddSpawnArea(){
		activeSpawnAreas ++;
	}

	private void EnemyDead(){
		GameObject sender = (GameObject)EventManager.GetSender(ConstantVar.IS_DEAD);
		if(sender == null){
			return;
		}
		
		for(int i=0; i<activeSpawnAreas; i++){
			SpawnArea spawn = spawnAreas[i];
			if(spawn.enemies.Contains(sender)){
				spawn.enemies.Remove(sender);
				SpawnEnemy(spawn);
				return;
			}
		}
	}

	private Vector3 GetRandomPointOnSpawnCircle(SpawnArea spawn)
	{
        circleRadius = (cam.orthographicSize * cam.aspect) + Mathf.Abs(cameraRig.position.x) + offset;
		
		float degree = UnityEngine.Random.Range(spawn.minAngle, spawn.maxAngle);
		float radian = degree * Mathf.Deg2Rad;
		float x = Mathf.Cos(radian);
		float y = Mathf.Sin(radian);
		
        Vector3 pos = new Vector3(x, y, 0) * circleRadius;

        return pos;
	}

	private IEnumerator ExpandSpawnArea(){
		while(activeSpawnAreas < spawnAreas.Count){
			yield return wait;

			AddSpawnArea();
		}
		StopCoroutine(expandSpawnArea);
	}

	[Serializable]
	public struct SpawnArea{
		public float minAngle;
		public float maxAngle;
		public float nextSpawn;
		public List<GameObject> enemies;

		public SpawnArea(float _minAngle, float _maxAngle){
			minAngle = _minAngle;
			maxAngle = _maxAngle;
			nextSpawn = 0;
			enemies = new List<GameObject>();
		}
	}
}

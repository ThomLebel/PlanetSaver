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
	[SerializeField] private List<Vector2> spawnAreas;
	[SerializeField] private int activeSpawnAreas = 1;

	private void Awake()
	{
		cam = Camera.main;
		cameraRig = cam.GetComponentInParent<Transform>();
		minAngle = Random.Range(0, 360);
		maxAngle = minAngle + angleInterval;

		spawnAreas = new List<Vector2>();

		for(int i = 0; i < 8; i++){
			spawnAreas.Add(new Vector2(minAngle, maxAngle));
			minAngle = maxAngle;
			maxAngle += angleInterval;
		}

		spawnAreas = Helpers.Shuffle<Vector2>(spawnAreas);
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
			int enemyIndex = Random.Range(0, enemies.Count);
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
		
		float degree = Random.Range(spawnAreas[index].x, spawnAreas[index].y);
		float radian = degree * Mathf.Deg2Rad;
		float x = Mathf.Cos(radian);
		float y = Mathf.Sin(radian);
		
        Vector3 pos = new Vector3(x, y, 0) * circleRadius;

        return pos;
	}
}

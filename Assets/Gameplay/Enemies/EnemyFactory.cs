using UnityEngine;
using VavilichevGD.Utils.Timing;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyFactory : MonoBehaviour
{
	[SerializeField] private Transform enemyContainer;
	[SerializeField] private Transform[] spawnPoints;
	[SerializeField] private Enemy enemyPrefab;
	[SerializeField] private float timeToSpawnEnemy;

	private SyncedTimer _timer;
	private DiContainer _diContainer;
	
	[Inject]
	private void Init(DiContainer diContainer)
	{
		_diContainer = diContainer;
	}

	private void Awake()
	{
		_timer = new SyncedTimer(TimerType.UpdateTick);

		for (int i = 1; i < PlayerPrefs.GetInt("CurrentWave"); i++)
		{
			if (timeToSpawnEnemy > 0.4f)
				timeToSpawnEnemy -= 0.1f;
		}

		_timer.Start(timeToSpawnEnemy);
	}

	private void OnEnable()
	{
		_timer.TimerFinished += SpawnEnemy;
	}

	private void OnDisable()
	{
		_timer.TimerFinished -= SpawnEnemy;
	}

	private void SpawnEnemy()
	{
		_diContainer.InstantiatePrefabForComponent<Enemy>(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity, enemyContainer);

		_timer.Start(timeToSpawnEnemy);
	}
}

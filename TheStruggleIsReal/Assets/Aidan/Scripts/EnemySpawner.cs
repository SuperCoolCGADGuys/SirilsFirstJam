using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private ObjectPooling enemyPool = null;
	[SerializeField] private Transform spawnPositions = null;
	[SerializeField] private int numOfWavesToWaitToSpawnExtraEnemy = 1;
	private int maxNoOfEnemiesToSpawn = 0;

	private void Awake()
	{
		if (enemyPool == null)
		{
			Debug.Log("Enemypool not found!");
		}
	}

	// Update is called once per frame
	void Update()
    {
		maxNoOfEnemiesToSpawn = (GameManager.Instance.EnemiesKilled / numOfWavesToWaitToSpawnExtraEnemy) + 1;

		if (GameManager.Instance.EnemiesAlive < maxNoOfEnemiesToSpawn && GameManager.Instance.EnemiesAlive <= enemyPool.poolSize)
		{
			Spawn();
		}

	}

	void Spawn()
	{
		// Retrieve a new enemy instance
		GameObject newEnemy = enemyPool.RetrieveInstance();
		GameManager.Instance.EnemiesAlive++;

		// Set it's position to one of the spawn positions
		int randomChild = Random.Range(0, spawnPositions.childCount);
		newEnemy.transform.position = spawnPositions.GetChild(randomChild).position;
	}
}

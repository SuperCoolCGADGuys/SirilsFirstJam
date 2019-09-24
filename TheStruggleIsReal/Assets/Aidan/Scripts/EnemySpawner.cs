using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private ObjectPooling enemyPool = null;
	[SerializeField] private Transform spawnPositions = null;
	[SerializeField] private int numOfWavesToWaitToSpawnExtraEnemy = 1;
	[SerializeField] private float spawningTime = 0;
	[SerializeField] private GameObject spawningParticles = null;
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
		int randomChild = Random.Range(0, spawnPositions.childCount);
		GameObject particles = Instantiate(spawningParticles, spawnPositions.GetChild(randomChild).position, Quaternion.identity);
		StartCoroutine(WaitForParticlesToFinish(particles, randomChild));
		GameManager.Instance.EnemiesAlive++;
	}

	IEnumerator WaitForParticlesToFinish(GameObject particles, int randomChild)
	{
		yield return new WaitForSeconds(spawningTime);
		Destroy(particles);

		// Retrieve a new enemy instance
		GameObject newEnemy = enemyPool.RetrieveInstance();

		// Set it's position to one of the spawn positions
		newEnemy.transform.position = spawnPositions.GetChild(randomChild).position;
	}

	public void ResetSpawner()
	{
		enemyPool.DevolveAll();
	}
}

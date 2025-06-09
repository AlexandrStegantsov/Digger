using System.Collections.Generic;
using UnityEngine;

public class EnemyAIGenerator : MonoBehaviour
{
    [Header("Pooling")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 20;
    private List<GameObject> enemyPool;

    [Header("Spawning")]
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        CreatePool();
        GenerateEnemies(20);
    }

    private void CreatePool()
    {
        enemyPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    private GameObject GetPooledEnemy()
    {
        foreach (var enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
                return enemy;
        }
        
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.SetActive(false);
        enemyPool.Add(newEnemy);
        return newEnemy;
    }

    public void GenerateEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = GetPooledEnemy();
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = Quaternion.identity;
            enemy.SetActive(true);
        }
    }
}
using Controller;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemyController
{
    public GameObject EnemyPrefab;
    public BoxCollider2D playerMovementArea;

    public EnemyDatabase enemyDatabase;

    [Header("Spawning Mechanics")]
    public float minSpawnDistance = 2f; // Minimum distance from the box
    public float maxSpawnDistance = 3f; // Maximum distance from the box
    public int spawnCount = 5; // Number of enemies to spawn

    [Header("Spawning Interval")]
    public float spawnInterval = 1.5f;
    public float curSpawnInterval;

    [Header("Current Spawned Enemies")]
    public List<GameObject> enemySpawned = new List<GameObject>();

    private Bounds bounds;
    private PlayerControllerManager currentPlayerTarget;

    public void SetCurrentPlayer(PlayerControllerManager newPlayer)
    {
        currentPlayerTarget = newPlayer;
    }

    public void Initialize(GameObject newEnemyPrefab, BoxCollider2D newPlayerMovementArea, EnemyDatabase newEnemyDatabase)
    {
        EnemyPrefab = newEnemyPrefab;

        playerMovementArea = newPlayerMovementArea;
        bounds = playerMovementArea.bounds;

        enemyDatabase = newEnemyDatabase;
    }

    public void Update(float deltaTime)
    {
        if (!GameManager.instance.inGame) return;
        
        if(curSpawnInterval <= 0.0f)
        {
            curSpawnInterval = spawnInterval;
            SpawnEnemy();
        }
        if(curSpawnInterval > 0.0f)
        {
            curSpawnInterval -= deltaTime;
        }

    }

    public void ResetSpawn()
    {
        enemySpawned.ForEach(x =>
        {
            x.SetActive(false);
        });

        curSpawnInterval = 0.0f;
    }

    void SpawnEnemy()
    {
        GameObject enemyGo = null;
        Vector2 randomPosition = GetRandomSpawnPosition();

        if (enemySpawned.Count == 0 || enemySpawned.All(x => x.activeSelf))
        {
            enemyGo = GameObject.Instantiate(EnemyPrefab, randomPosition, Quaternion.identity);
            enemySpawned.Add(enemyGo);

        }
        else
        {
            enemyGo = enemySpawned.First(x => !x.activeSelf);
            enemyGo.transform.position = randomPosition;
            enemyGo.gameObject.SetActive(true);
        }

        EnemyControllerManager enemy = enemyGo.GetComponent<EnemyControllerManager>();
        enemy.OnEnemyHitEvent.AddListener(UiManager.instance.killCountView.IncrementKillCount);
        enemy.OnEnemyHitVfxEvent.AddListener(SpawnEnemyVisualEffects);
        enemy.InitializeInfo(enemyDatabase.GetNewCharacterInfo(), currentPlayerTarget);
    }

    void SpawnEnemyVisualEffects(EnemyControllerManager enemyOwner)
    {
        Vector2 position = enemyOwner.transform.position;
        GameManager.instance.spawnVfxController.SpawnEffects(position, enemyOwner.onDamagedVFX);
        GameManager.instance.spawnVfxController.SpawnEffects(position, enemyOwner.onDeathVFX);
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition = Vector2.zero;
        bool validSpawn = false;

        while (!validSpawn)
        {
            // Choose a random position outside the box
            float randomX, randomY;

            // Randomly pick a side (Top, Bottom, Left, Right)
            int side = Random.Range(0, 4);

            switch (side)
            {
                case 0: // Top side
                    randomX = Random.Range(bounds.min.x - maxSpawnDistance, bounds.max.x + maxSpawnDistance);
                    randomY = bounds.max.y + Random.Range(minSpawnDistance, maxSpawnDistance);
                    break;

                case 1: // Bottom side
                    randomX = Random.Range(bounds.min.x - maxSpawnDistance, bounds.max.x + maxSpawnDistance);
                    randomY = bounds.min.y - Random.Range(minSpawnDistance, maxSpawnDistance);
                    break;

                case 2: // Left side
                    randomX = bounds.min.x - Random.Range(minSpawnDistance, maxSpawnDistance);
                    randomY = Random.Range(bounds.min.y - maxSpawnDistance, bounds.max.y + maxSpawnDistance);
                    break;

                case 3: // Right side
                    randomX = bounds.max.x + Random.Range(minSpawnDistance, maxSpawnDistance);
                    randomY = Random.Range(bounds.min.y - maxSpawnDistance, bounds.max.y + maxSpawnDistance);
                    break;

                default:
                    randomX = bounds.min.x; randomY = bounds.min.y;
                    break;
            }

            spawnPosition = new Vector2(randomX, randomY);

            // Check if the position is outside the movement area
            if (!bounds.Contains(spawnPosition))
                validSpawn = true;
        }

        return spawnPosition;
    }
}

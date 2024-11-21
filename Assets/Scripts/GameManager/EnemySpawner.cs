using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    public int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    float timer = 0;

    void Start()
    {
        ResetSpawner();
    }

    void Update()
    {
        if (isSpawning)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval && spawnCount > 0)
            {
                SpawnEnemy();
                timer = 0;
            }
        }
    }

    public void SpawnEnemy()
    {
        Enemy enemy = Instantiate(spawnedEnemy);
        enemy.spawner = this;
        totalKillWave++;
        combatManager.totalEnemies++;
        spawnCount--;
    }

    public void ResetSpawner()
    {
        spawnCount = defaultSpawnCount + (spawnCountMultiplier - 1) * multiplierIncreaseCount;
        totalKillWave = 0;
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave--;

        if (totalKill % minimumKillsToIncreaseSpawnCount == 0)
        {
            spawnCountMultiplier++;
        }

        if (totalKillWave <= 0)
        {
            combatManager.CheckWaveCompletion();
        }
    }
}

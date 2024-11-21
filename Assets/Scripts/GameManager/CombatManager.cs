using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    bool waveInProgress = false;

    void Update()
    {
        if (!waveInProgress)
            timer += Time.deltaTime;
        

        if (!waveInProgress && timer >= waveInterval)
        {
            StartNextWave();
            timer = 0; // Reset timer for next wave
        }
    }

    void StartNextWave()
    {
        waveNumber++;
        totalEnemies = 0;
        waveInProgress = true;

        foreach (var spawner in enemySpawners)
        {
            spawner.ResetSpawner();
            spawner.isSpawning = true;
        }
    }

    public void CheckWaveCompletion()
    {
        foreach (var spawner in enemySpawners)
        {
            if (spawner.totalKillWave > 0)
                return; // Still enemies in current wave
        }

        waveInProgress = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LevelManager = GetComponentInChildren<LevelManager>();

        if (LevelManager == null)
        {
            Debug.LogWarning(this + "tidak memiliki LevelManager");
        }

        DontDestroyOnLoad(gameObject);//Menyimpan Game Manager saat berganti screen
        GameObject mainCamera = GameObject.Find("Main Camera");
        if (mainCamera != null)
        {
            DontDestroyOnLoad(mainCamera);//Menyimpan MainCamera saat berganti screen
        }
        else
        {
            Debug.LogWarning(this + "tidak menemukan Main Camera");
        }
        GameObject enemyClickSpawner = GameObject.Find("EnemyClickSpawner");
        if (enemyClickSpawner != null)
        {
            DontDestroyOnLoad(enemyClickSpawner);//Menyimpan EnemyClickSpawner saat berganti screen
        }
        else
        {
            Debug.LogWarning(this + "tidak menemukan EnemyClickSpawner");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : EnemyHorizontal
{
    public BigBoolet bigBoolet;
    public Transform bulletSpawnPoint;  // Where bullets will spawn from.
    public float shootInterval = 2f;    // How often the boss shoots.

    private float shootTimer;

    [Header("Bullet Pool")]
    private IObjectPool<BigBoolet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;

    public override void Awake()
    {
        base.Awake();
        // Initialize the bullet object pool
        objectPool = new ObjectPool<BigBoolet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );

        // Find BulletSpawnPoint if not assigned in the Inspector
        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = transform.Find("BulletSpawnPoint");

            if (bulletSpawnPoint == null)
            {
                Debug.LogWarning("BulletSpawnPoint not found as a child of Weapon.");
            }
            else
            {
                bulletSpawnPoint.position = new Vector3(0, 1, 0); // Initial offset
            }
        }
    }

    new void Start()
    {
        base.Start();  // Ensure base Start() is called for movement initialization.
        shootTimer = 0;
    }

    public override void Move()
    {
        base.Move();  // Use the horizontal movement logic from EnemyHorizontal.

        // Handle shooting at intervals.
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    private BigBoolet CreateBullet()
    {
        BigBoolet newBoolet = Instantiate(bigBoolet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBoolet.SetObjectPool(objectPool); // Assign pool to bullet
        return newBoolet;
    }

    private void OnGetBullet(BigBoolet bigBoolet)
    {
        bigBoolet.gameObject.SetActive(true); // Activate bullet
        bigBoolet.transform.position = bulletSpawnPoint.position;
        bigBoolet.transform.rotation = bulletSpawnPoint.rotation;
    }

    private void OnReleaseBullet(BigBoolet bigBoolet)
    {
        bigBoolet.gameObject.SetActive(false); // Deactivate bullet
    }

    private void OnDestroyBullet(BigBoolet bigBoolet)
    {
        Destroy(bigBoolet.gameObject); // Destroy bullet when pool limit is reached
    }
    
    public BigBoolet Shoot()
    {
        if (objectPool != null && bulletSpawnPoint != null)
        {
            BigBoolet bulletInstance = objectPool.Get();
            return bulletInstance;
        }
        return null;
    }
}

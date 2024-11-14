using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet; // Prefab reference for the Bullet
    [SerializeField] private Transform bulletSpawnPoint; // Spawn point for the bullet

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;

    public Transform parentTransform;

    void Awake()
    {
        // Initialize the object pool with custom create, release, and destroy methods
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );

        // Ensure bulletSpawnPoint is set
        if (bulletSpawnPoint == null)
        {
            Debug.LogWarning("Bullet Spawn Point is not set!");
        }
    }

    void Update()
    {
        // Timer to control shooting interval
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0;
        }
    }

    // Method to spawn a bullet from the object pool
    public Bullet Shoot()
    {
        if (objectPool != null)
        {
            Bullet bulletInstance = objectPool.Get();
            return bulletInstance;
        }
        return null;
    }

    // Create a new bullet instance when the pool needs more objects
    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.SetObjectPool(objectPool);
        return newBullet;
    }

    // Called when getting a bullet from the pool
    private void OnGetBullet(Bullet bullet)
    {
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.gameObject.SetActive(true);
    }

    // Called when releasing a bullet back to the pool
    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    // Called when destroying a bullet in the pool
    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}

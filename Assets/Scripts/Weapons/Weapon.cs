using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    public Transform parentTransform;

    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;
    
    [Header("Bullets")] 
    public BigBoolet bigBoolet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<BigBoolet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;

    void Awake() 
    {
        objectPool = new ObjectPool<BigBoolet>(
            CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet,
            collectionCheck, defaultCapacity, maxSize
        );

        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = transform.Find("BulletSpawnPoint");

            if (bulletSpawnPoint == null)
            {
                Debug.LogWarning("BulletSpawnPoint not found");
            }
            else
            {
                bulletSpawnPoint.position = new Vector3(0,1,0);
            }
        }
    }

    BigBoolet CreateBullet()
    {
        BigBoolet newBoolet = Instantiate(bigBoolet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBoolet.SetObjectPool(objectPool);
        return newBoolet;
    }

    void OnGetBullet(BigBoolet bigBoolet)
    {
        bigBoolet.gameObject.SetActive(true);
        bigBoolet.transform.position = bulletSpawnPoint.position;
        bigBoolet.transform.rotation = bulletSpawnPoint.rotation;
    }

    void OnReleaseBullet(BigBoolet bigBoolet)
    {
        bigBoolet.gameObject.SetActive(false);
    }

    void OnDestroyBullet(BigBoolet bigBoolet)
    {
        Destroy(bigBoolet.gameObject);
    }

    void Update() 
    {
        timer += Time.deltaTime;

        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0;
        }
    }

    public BigBoolet Shoot()
    {
        if (objectPool != null)
        {
            BigBoolet bulletInstance = objectPool.Get();
            return bulletInstance;
        }
        return null;
    }
}

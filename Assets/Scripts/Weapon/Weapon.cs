using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    public Transform parentTransform;
    public WeaponPickup weaponPickup;

    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;

    void Awake()
    {
        // Membuat object pool
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );

        // Memeriksa apakah BulletSpawnPoint telah ditambahkan
        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = transform.Find("BulletSpawnPoint");

            if (bulletSpawnPoint == null)
            {
                Debug.LogWarning(this + " tidak menemukan BulletSpawnPoint");
            }
            else
            {
                // Memposisikan BulletSpawnPoint didepan Player agar bullet tidak mengenai Player
                bulletSpawnPoint.position = new Vector3(0,1,0);
            }
        }
    }

    // Menambahkan Bullet ke ObjectPool
    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.SetObjectPool(objectPool);
        return newBullet;
    }

    // Mengaktifkan Bullet di dalam pool yang akan digunakan
    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = bulletSpawnPoint.position; // Mereset posisi bullet
        bullet.transform.rotation = bulletSpawnPoint.rotation; // Mereset rotasi bullet
    }

    // Menonaktifkan Bullet yang sudah tidak digunakan
    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    // Menghancurkan Bullet jika ObjectPool sudah penuh
    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Menembakkan Bullet dalam interval waktu
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0;
        }
    }

    public Bullet Shoot()
    {
        if (objectPool != null)
        {
            Bullet bulletInstance = objectPool.Get(); // Mengambil Bullet dari ObjectPool
            return bulletInstance;
        }
        return null;
    }
}

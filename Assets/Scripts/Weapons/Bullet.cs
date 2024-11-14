using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;

    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;

    public void SetObjectPool(IObjectPool<Bullet> pool)
    {
        objectPool = pool;
    }

    void OnEnable()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Start the bullet moving forward
        rb.velocity = transform.up * bulletSpeed;
    }

    // When the bullet collides with any object, return it to the pool
    void OnCollisionEnter2D(Collision2D collision)
    {
        objectPool?.Release(this);
    }

    // When the bullet leaves the screen, return it to the pool
    void OnBecameInvisible()
    {
        objectPool?.Release(this);
    }
}

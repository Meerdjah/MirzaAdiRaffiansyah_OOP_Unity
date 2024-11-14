using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BigBoolet : MonoBehaviour
{
    [Header("Big Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<BigBoolet> objectPool;

    private bool isActive;

    public void SetObjectPool(IObjectPool<BigBoolet> pool)
    {
        objectPool = pool;
    }

    void OnEnable()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        isActive = false;  // Bullet is inactive until picked up
        rb.velocity = Vector2.zero;
    }

    public void ActivateBullet()  // Call this method to start moving the bullet
    {
        isActive = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        objectPool?.Release(this);
    }

    void OnBecameInvisible()
    {
        objectPool?.Release(this);
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            rb.velocity = transform.up * bulletSpeed;  // Move bullet only if active
        }
    }
}
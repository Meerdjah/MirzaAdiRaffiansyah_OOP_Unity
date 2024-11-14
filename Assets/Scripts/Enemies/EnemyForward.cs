using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    public override void Awake()
    {
        base.Awake();
        transform.position = new Vector3(Random.Range(-8f, 8f), 6f, 0);
    }

    public override void Move()
    {
        rb.velocity = new Vector2(0, -moveSpeed);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

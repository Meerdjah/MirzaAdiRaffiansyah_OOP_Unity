using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    public Transform player;

    public override void Awake()
    {
        base.Awake();
        
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        transform.position = new Vector3(Random.Range(-8f, 8f), 6f, 0);
    }

    public override void Move()
    {
        if (player == null) return;
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

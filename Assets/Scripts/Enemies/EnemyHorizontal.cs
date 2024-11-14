using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private bool movingRight = true;
    private Vector3 screenBounds;

    public override void Awake()
    {
        base.Awake();
        float spawnX = Random.Range(-screenBounds.x, screenBounds.x);
        transform.position = new Vector3(spawnX, transform.position.y, 0);
    }
    public void Start()
    {
        if (mainCamera != null)
        {
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        }
        else
        {
            Debug.LogError("mainCamera is not assigned in EnemyHorizontal.");
        }
    }

    public override void Move()
    {
        if (mainCamera == null) return;

        rb.velocity = new Vector2(movingRight ? moveSpeed : -moveSpeed, rb.velocity.y);

        if (transform.position.x > screenBounds.x)
        {
            movingRight = false;
        }
        else if (transform.position.x < -screenBounds.x)
        {
            movingRight = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;
    public Sprite enemySprite;
    protected Rigidbody2D rb;
    public float moveSpeed = 2f;

    public Camera mainCamera;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("No main camera found. Assign a camera in the Inspector.");
            }
        }
    }

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public virtual void Move()
    {
    }

    void Update()
    {
        Move();
    }
}

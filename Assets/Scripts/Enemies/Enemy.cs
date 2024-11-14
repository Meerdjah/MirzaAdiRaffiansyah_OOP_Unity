using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;
    public Sprite enemySprite;
    protected Rigidbody2D rb;
    public float moveSpeed = 2f;

    public Camera mainCamera;

    // Akan dioverride oleh child class
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        // Memberikan MainCamera ke Enemy
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Tidak ada camera pada " + this);
            }
        }
    }

    // Memberikan sprite untuk Enemy
    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public virtual void Move()
    {
        // Akan dioverride oleh child class
    }

    void Update()
    {
        Move();
    }
}
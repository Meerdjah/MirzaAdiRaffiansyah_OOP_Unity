using UnityEngine;

public class EnemyForward : Enemy
{
    public override void Awake()
    {
        base.Awake();

        if (mainCamera != null)
        {
            // Menentukan posisi spawn
            float spawnX = Random.Range(0, Screen.width);
            Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(new Vector3(spawnX, Screen.height, mainCamera.transform.position.z));
            transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        }
        else
        {
            Debug.LogError(this + " tidak memiliki MainCamera");
        }
    }

    public override void Move()
    {
        rb.velocity = new Vector2(0, -moveSpeed);  // Menggerakkan EnemyForward ke bawah
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);  // Menghancurkan EnemyForward saat keluar dari layar
    }
}
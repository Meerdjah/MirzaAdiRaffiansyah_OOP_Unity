using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    [SerializeField] float rotateSpeed = 100f;
    GameObject player;
    SpriteRenderer spriteRenderer;
    Collider2D portalCollider;
    Vector2 newPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        portalCollider = GetComponent<Collider2D>();

        player = GameObject.FindWithTag("Player");

        spriteRenderer.enabled = false;
        portalCollider.enabled = false;

        ChangePosition();
    }

    void Update()
    {
        if (player.GetComponentInChildren<Weapon>() == null)
        {
            return;
        }

        spriteRenderer.enabled = true;
        portalCollider.enabled = true;

        Vector2 currentPosition = transform.position;
        Vector2 nextPosition = Vector2.MoveTowards(currentPosition, newPosition, speed * Time.deltaTime);

        transform.position = nextPosition;

        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }
    void ChangePosition()
    {
        float randomX = Random.Range(-8f, 8f);
        float randomY = Random.Range(-4.5f, 4.5f);
        newPosition = new Vector2(randomX, randomY);
    }

}
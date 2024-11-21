using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    Vector2 newPosition;    

    GameObject player;     
    SpriteRenderer spriteRenderer;
    Collider2D portalCollider;

    void Start()
    {
        //Mengambil informasi komponen sprite renderer dan collider2D
        spriteRenderer = GetComponent<SpriteRenderer>();
        portalCollider = GetComponent<Collider2D>();

        // Mencari objek player di dalam game
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning(this + "tidak menemukan Player");
            return;
        }

        //Menghilangkan portal dari scene
        spriteRenderer.enabled = false;
        portalCollider.enabled = false;

        // Menginisialisasi arah pergerakan portal
        ChangePosition();
    }

    void Update()
    {
        //Memeriksa apakah player telah equip weapon
        if (player.GetComponentInChildren<Weapon>() == null || player == null)
        {
            return;
        }

        //mengaktifkan sprite dan collider dari portal
        spriteRenderer.enabled = true;
        portalCollider.enabled = true;

        // Menggerakkan portal
        Vector2 currentPosition = transform.position;
        Vector2 nextPosition = Vector2.MoveTowards(currentPosition, newPosition, speed * Time.deltaTime);

        transform.position = nextPosition;

        //Debug.Log($"Current Position: {currentPosition}, Next Position: {nextPosition}, Speed: {speed}");

        // Melakukan rotasi pada portal
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        // Mengubah arah pergerakan jika sudah hampir sampai tujuan
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            newPosition = ChangePosition();
        }
    }

    //Method untuk memberikan arah tujuan dari portal
    Vector2 ChangePosition()
    {
        float randomX = Random.Range(-8f, 8f);
        float randomY = Random.Range(-4.5f, 4.5f);
        return new Vector2(randomX, randomY);
    }

    //Mengganti scene jika portal disentuh player
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the portal collides with the player, load the Main scene
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }
}

using System.Collections;

using UnityEngine;

public class InvisibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;
    private int count;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // menyimpan material yang sedang digunakan
        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());// Menjalankan coroutine untuk efek blinking
    }

    private IEnumerator FlashRoutine()
    {
        count = 0;
        isInvincible = true; // Fase invincible
        // Blinking sebanyak blinkingCount
        while (count < blinkingCount)
        {
            spriteRenderer.material = blinkMaterial; // Mengubah material

            // Delay blink
            yield return new WaitForSeconds(blinkInterval);

            spriteRenderer.material = originalMaterial; // Mengembalikan material 
            count++; 
        }
        isInvincible = false; // Fase invincible selesai
    }

}

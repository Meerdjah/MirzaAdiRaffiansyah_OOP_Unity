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

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer is missing on this GameObject. Invisibility effects will not work.");
        }
    }

    void Start()
    {
        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
    }

    public void Flash()
    {
        // Start blinking if not already invincible
        if (!isInvincible && spriteRenderer != null)
        {
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        if (spriteRenderer == null || blinkMaterial == null)
        {
            yield break; // Exit if SpriteRenderer or blinkMaterial is missing
        }

        count = 0;
        isInvincible = true; // Start invincibility phase

        while (count < blinkingCount)
        {
            spriteRenderer.material = blinkMaterial; // Change to blink material

            yield return new WaitForSeconds(blinkInterval); // Blink delay

            spriteRenderer.material = originalMaterial; // Revert to original material
            count++;
        }

        isInvincible = false; // End invincibility phase
    }
}

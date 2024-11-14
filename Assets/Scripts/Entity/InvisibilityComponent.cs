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
        // Get the SpriteRenderer to be used,
        // alternatively you could set it from the inspector.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the material that the SpriteRenderer uses, 
        // so we can switch back to it after the flash ended.
        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        count = 0;
        isInvincible = true;
        // Swap to the flashMaterial.
        while (count < blinkingCount)
        {
            spriteRenderer.material = blinkMaterial;

            // Pause the execution of this function for "duration" seconds.
            yield return new WaitForSeconds(blinkInterval);

            // After the pause, swap back to the original material.
            spriteRenderer.material = originalMaterial;  
            count++; 
        }
        isInvincible = false;
    }

}

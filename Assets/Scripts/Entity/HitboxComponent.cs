using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    public HealthComponent health;
    InvisibilityComponent invisibilityComponent;

    void Start()
    {
        if (health == null)
        {
            health = GetComponent<HealthComponent>();
        }
    }

    public void Damage(int amount)
    {
        invisibilityComponent = GetComponent<InvisibilityComponent>();
        if (!invisibilityComponent.isInvincible)
        {
            health?.Subtract(amount);
            invisibilityComponent.Flash();
        }
        
    }
}

using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int maxHealth;
    private int health{ get; set;}

    void Start()
    {
        health = maxHealth;
    }

    public void Subtract(int amount)
    {
        health -= amount;
        Debug.Log(this + " Getting hit by " + amount + " damage. remaining health : " + health);
        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }
    }
}

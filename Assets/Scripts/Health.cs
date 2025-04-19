using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float maxHealth;

    public Health(float? health)
    {
        this.health = health ?? 100f;
        this.maxHealth = health ?? 100f;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}

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

    public bool TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
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

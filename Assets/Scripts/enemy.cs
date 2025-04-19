using UnityEngine;

public class enemy : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            DealWithDamageTaken(damageDealer);
        }
    }

    private void DealWithDamageTaken(DamageDealer damageDealer)
    {
        Health health = GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(damageDealer.GetDamage());
        }
        damageDealer.Hit();
    }
}

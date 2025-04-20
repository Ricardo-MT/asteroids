using UnityEngine;

public class enemy : MonoBehaviour
{

    [SerializeField] ParticleSystem onDestroyedEffectPrefab;
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
            bool died = health.TakeDamage(damageDealer.GetDamage());
            if (died)
            {
                PlayDiedEffect();
            }
        }
        damageDealer.Hit();
    }

    private void PlayDiedEffect()
    {
        // Play hit effect here
        ParticleSystem particleSystem = Instantiate(onDestroyedEffectPrefab, transform.position, Quaternion.identity);
        Destroy(particleSystem.gameObject, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
    }
}

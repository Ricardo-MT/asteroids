using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] ParticleSystem onDestroyedEffectPrefab;

    AudioPlayer audioPlayer;

    Action onEnemyDestroyed;

    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    public Action OnEnemyDestroyed
    {
        get => onEnemyDestroyed;
        set => onEnemyDestroyed = value;
    }

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
                HandleOnDestroy();
            }
        }
        damageDealer.Hit();
    }

    private void HandleOnDestroy()
    {
        audioPlayer.PlayExplosionEnemy();
        PlayDiedEffect();
    }

    private void PlayDiedEffect()
    {
        ParticleSystem particleSystem = Instantiate(onDestroyedEffectPrefab, transform.position, Quaternion.identity);
        Destroy(particleSystem.gameObject, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
    }

    void OnDestroy()
    {
        onEnemyDestroyed?.Invoke();
    }
}

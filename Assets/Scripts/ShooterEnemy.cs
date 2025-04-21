
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] float fireRate = 0f;

    AudioPlayer audioPlayer;

    float lastFireTime = 0f;

    void Awake()
    {
        float minFireRate = 1.3f;
        float maxFireRate = 2.7f;
        fireRate = Random.Range(minFireRate, maxFireRate);
    }

    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        lastFireTime = Time.time;
    }

    void Update()
    {
        HandleFire();
    }

    void HandleFire()
    {
        bool canFire = (Time.time - lastFireTime) > fireRate;
        if (canFire)
        {
            Fire();
            audioPlayer.PlayShootingClipEnemy();
            lastFireTime = Time.time;
        }
    }

    void Fire()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        var projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.Direction = -1 * transform.up;
        projectileComponent.Speed = 10f;
    }
}

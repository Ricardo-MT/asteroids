
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] float fireRate = 0f;


    float lastFireTime = 0f;

    void Start()
    {
        float minFireRate = 0.7f;
        float maxFireRate = 2.3f;
        fireRate = Random.Range(minFireRate, maxFireRate);
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

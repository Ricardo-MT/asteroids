using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] float fireRate = 0.12f;

    bool isFiring = false;

    bool canFire = true;
    float lastFireTime = 0f;

    public bool IsFiring
    {
        get { return isFiring; }
        set { isFiring = value; }
    }

    public bool CanFire
    {
        get { return canFire; }
        set { canFire = value; }
    }

    public float LastFireTime
    {
        get { return lastFireTime; }
        set { lastFireTime = value; }
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public void Fire()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        var projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.Direction = transform.up;
    }
}

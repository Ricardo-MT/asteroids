using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage;

    public DamageDealer(float? damage)
    {
        this.damage = damage ?? 10f;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 30f;
    float lifetime = 4f;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the projectile after its lifetime
    }

    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = speed * Time.deltaTime * direction;
        if (gameObject.name.Contains("Enemy"))

        {
            Debug.Log("Enemy component found " + direction + delta);
        }
        else
        {
            Debug.Log("Player component found " + direction + delta);
        }
        Vector3 newPosition = transform.position + delta;
        transform.position = newPosition;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    [Header("Player movement")]
    [SerializeField] float speed = 10f;
    Vector2 rawInput;

    Vector2 minBounds;
    Vector2 maxBounds;

    [Header("Player bounds")]
    [SerializeField] float paddingLeft = 0.5f;
    [SerializeField] float paddingRight = 0.5f;
    [SerializeField] float paddingTop = 0.5f;
    [SerializeField] float paddingBottom = 1f;

    Shooter shooter;

    CameraShake cameraShake;

    AudioPlayer audioPlayer;

    MyEventManager eventManager;

    LevelManagerScript levelManager;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitBounds();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        eventManager = FindObjectOfType<MyEventManager>();
        levelManager = FindObjectOfType<LevelManagerScript>();
    }

    void InitBounds()
    {
        Camera cam = Camera.main;
        minBounds = cam.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = cam.ViewportToWorldPoint(new Vector2(1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        HandleFire();
    }

    private void HandleFire()
    {
        shooter.CanFire = (Time.time - shooter.LastFireTime) > shooter.GetFireRate();
        if (shooter.IsFiring && shooter.CanFire)
        {
            shooter.Fire();
            audioPlayer.PlayShootingClipPlayer();
            shooter.LastFireTime = Time.time;
        }
    }

    private void UpdateMove()
    {
        Vector3 delta = speed * Time.deltaTime * rawInput;

        Vector2 newPos = transform.position + delta;
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        shooter.IsFiring = value.isPressed;
        if (!value.isPressed)
        {
            shooter.LastFireTime = 0f;
        }
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
            eventManager.Trigger(new PlayerHealthChanged(health.GetHealth(), health.GetMaxHealth()));
            if (died)
            {
                HandleOnDestroy();
            }
            else
            {
                HandleOnHit();
            }
        }
        damageDealer.Hit();
    }

    private void HandleOnHit()
    {
        cameraShake.Shake();
        audioPlayer.PlayHitPlayer();
    }

    private void HandleOnDestroy()
    {
        audioPlayer.PlayExplosionPlayer();
        levelManager.LoadGameOver();
    }
}

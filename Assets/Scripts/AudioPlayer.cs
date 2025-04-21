using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("PLayer")]
    [SerializeField] AudioClip shootingClipPlayer;
    [SerializeField] AudioClip explosionPlayer;
    [SerializeField] AudioClip hitPlayer;

    [Header("Enemy")]
    [SerializeField] AudioClip shootingClipEnemy;
    [SerializeField] AudioClip explosionEnemy;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;

    GameObject listener;

    void Start()
    {
        listener = this.gameObject;
    }

    public void PlayShootingClipPlayer()
    {
        AudioSource.PlayClipAtPoint(shootingClipPlayer, listener.transform.position, shootingVolume);
    }

    public void PlayShootingClipEnemy()
    {
        AudioSource.PlayClipAtPoint(shootingClipEnemy, listener.transform.position, shootingVolume);
    }

    public void PlayExplosionPlayer()
    {
        AudioSource.PlayClipAtPoint(explosionPlayer, listener.transform.position, shootingVolume);
    }
    public void PlayExplosionEnemy()
    {
        AudioSource.PlayClipAtPoint(explosionEnemy, listener.transform.position, shootingVolume);
    }

    public void PlayHitPlayer()
    {
        AudioSource.PlayClipAtPoint(hitPlayer, listener.transform.position, shootingVolume);
    }
}

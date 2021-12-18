using UnityEngine;
public class Shootable : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip bulletHitSoundEffect;
    public float maxPitch = 1.2f;
    public float minPitch = 0.8f;
    private AudioSource audioSource;

    public float volume = 1f;
    [Header("Settings")]
    public bool addPoints = true;
    public bool bonusPoints = true;
    public bool removePoints = true;
    [Header("Destroyable")]
    public bool shouldDestroy = true;
    

    public virtual void Hit(RaycastHit hit, Projectile bullet, Vector3 rayDirection)
    {
        bool gameEnded = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetCurrentGameEnded();
        print("HIT");

        if (audioSource == null)
        {
            if (!GetComponent<AudioSource>())
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                audioSource = GetComponent<AudioSource>();
            }

            audioSource.clip = bulletHitSoundEffect;
        }

        if (GetComponent<Rigidbody>() && !shouldDestroy)
        {
            var rb = GetComponent<Rigidbody>();
            var impactForce = bullet.bulletMass * Mathf.Pow(bullet.bulletVelocity, 2) * rayDirection;
            rb.AddForceAtPosition(impactForce, hit.point);
        }

        if (bulletHitSoundEffect != null)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.volume = volume;
            audioSource.Play();
        }

        if (shouldDestroy && !gameEnded)
        {
            if (removePoints)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().RemoveFromScore(10);
            }

            if (bonusPoints)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().AddToScore(20);
            }

            if (addPoints)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().AddToScore(10);
            }

            Destroy(gameObject);
        }
    }
    public virtual void Hit(RaycastHit hit, Arrow bullet, Vector3 rayDirection)
    {
        bool gameEnded = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetCurrentGameEnded();
        print("HIT");

        if (audioSource == null)
        {
            if (!GetComponent<AudioSource>())
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                audioSource = GetComponent<AudioSource>();
            }

            audioSource.clip = bulletHitSoundEffect;
        }

        if (bulletHitSoundEffect != null)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.volume = volume;
            audioSource.Play();
        }

        if (shouldDestroy && !gameEnded)
        {
            if (removePoints)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().RemoveFromScore(10);
            }

            if (bonusPoints)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().AddToScore(20);
            }

            if (addPoints)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().AddToScore(10);
            }

            Destroy(gameObject);
        }
    }

}
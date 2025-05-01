using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public GameObject healthPrefab;
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 15f;
    public float spawnRadius = 5f;
    public AudioClip pizzaSpawnSound;
    [Range(0f, 1f)] public float spawnVolume = 1f;

    private float timer;
    private float currentSpawnInterval;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        SetRandomSpawnInterval();
        TrySpawnHealth(false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentSpawnInterval)
        {
            TrySpawnHealth(true); 
            timer = 0f;
            SetRandomSpawnInterval();
        }
    }

    void SetRandomSpawnInterval()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void TrySpawnHealth(bool playSound)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, spawnRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Health"))
                return;
        }

        SpawnHealth(playSound);
    }

    void SpawnHealth(bool playSound)
    {
        Instantiate(healthPrefab, transform.position, Quaternion.identity);

        if (playSound && pizzaSpawnSound != null)
        {
            audioSource.PlayOneShot(pizzaSpawnSound, spawnVolume);
        }
    }
}

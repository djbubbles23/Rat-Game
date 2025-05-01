using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public GameObject healthPrefab;
    public float spawnInterval = 10f;
    public float spawnRadius = 5f; // Radius around the spawn point to check for existing health objects

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            TrySpawnHealth();
            timer = 0f;
        }
    }

    void TrySpawnHealth()
    {
        // Check if there's any health object within a specific radius of the spawn position
        Collider[] colliders = Physics.OverlapSphere(transform.position, spawnRadius);

        bool canSpawn = true;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Health"))
            {
                canSpawn = false;
                break;
            }
        }

        if (canSpawn)
        {
            SpawnHealth();
        }
    }

    void SpawnHealth()
    {
        Instantiate(healthPrefab, transform.position, Quaternion.identity);
    }
}

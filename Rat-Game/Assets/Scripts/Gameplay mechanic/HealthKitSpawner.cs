using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public GameObject healthPrefab;
    public float spawnInterval = 10f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && GameObject.FindWithTag("Health") == null)
        {
            SpawnHealth();
            timer = 0f;
        }
    }

    void SpawnHealth()
    {
        Instantiate(healthPrefab, transform.position, Quaternion.identity);
    }
}

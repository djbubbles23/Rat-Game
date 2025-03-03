using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;                // reference to the player
    public GameObject enemyPrefab;          // enemy prefab (can make a list if more 1 enemy type)
    public float minSpawnDistance = 5f;    // closest enemy can spawn from player on x-axis
    public float maxSpawnDistance = 10f;    // furthest enemy can spawn from player on x-axis
    public float maxZDistance = 4f;         // width of the plane
    public float startSpawnRate = 5f;       // time between enemy spawns (seconds)
    public float growthRate = 0.05f;        // rate at which spawn rate speeds up 
    
    private Vector3 playerPos;              // player's current position
    private float spawnTimer;               // time until next enemy spawns
    private int spawnCount;                 // number of total enemies spawned

    private void Start()
    {
        // set spawn timer equal to first spawn rate
        spawnTimer = startSpawnRate;
    }
    
    private void Update()
    {
        // grab player's current position
        playerPos = player.position;
        
        // handle spawning new enemies
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            
            // set next spawn time to startSpawnRate * e^-kt, ensures spawn rate increases over time
            spawnTimer = startSpawnRate * Mathf.Exp(-growthRate * spawnCount);
            Debug.Log("Next spawn: " + spawnTimer);
        }
    }

    private void SpawnEnemy()
    {
        // calculate random position away from the player
        float xPosition = player.position.x;
        bool direction = Random.value < 0.5f;   // infront or behind player
        
        if (direction) { xPosition += Random.Range(minSpawnDistance, maxSpawnDistance); }
        else { xPosition -= Random.Range(minSpawnDistance, maxSpawnDistance); }
        
        // random z position within stage bounds
        float zPosition = player.position.z + Random.Range(-maxZDistance, maxZDistance);
        
        // create enemy at random position
        Vector3 spawnPosition = new Vector3(xPosition, 1f, zPosition);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnCount++;
    }
    
}

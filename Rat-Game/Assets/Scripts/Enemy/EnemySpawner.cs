using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;       // enemy prefab (can make a list if more 1 enemy type)
    public float minSpawnDistance = 5f;     // closest enemy can spawn from player on x-axis
    public float maxSpawnDistance = 10f;    // furthest enemy can spawn from player on x-axis
    public float maxZDistance = 4f;         // width of the plane
    public float spawnHeight = 2.5f;        // spawn height
    public float spawnRate = 0f;            // time between enemy spawns (seconds)
    public float growthRate = 0.05f;        // rate at which spawn rate speeds up 
    
    private Transform player;                // reference to the player
    private Vector3 playerPos;              // player's current position
    private float spawnTimer;               // time until next enemy spawns
    [SerializeField] public int enemyCount = 1; // number of total enemies alive

    private void Start()
    {
        // set spawn timer equal to first spawn rate
        spawnTimer = spawnRate;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void Update()
    {
        // grab player's current position
        playerPos = player.position;
        
        // handle spawning new enemies
        // spawnTimer -= Time.deltaTime;
        // if (spawnTimer <= 0)
        // {
        //     SpawnEnemy();
        //     
        //     spawnTimer = spawnRate;
        //     // Debug.Log("Next spawn: " + spawnTimer);
        // }

        
        // print(EnemiesAlive());
    }


    private void SpawnEnemy(GameObject enemy)
    {
        // calculate random position away from the player
        float xPosition = player.position.x;
        bool direction = Random.value < 0.5f;   // infront or behind player
        
        if (direction) { xPosition += Random.Range(minSpawnDistance, maxSpawnDistance); }
        else { xPosition -= Random.Range(minSpawnDistance, maxSpawnDistance); }
        
        // random z position within stage bounds
        float zPosition = player.position.z + Random.Range(-maxZDistance, maxZDistance);
        
        // create enemy at random position
        Vector3 spawnPosition = new Vector3(xPosition, spawnHeight, zPosition);
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    public void SpawnEnemies(GameObject[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            SpawnEnemy(enemies[i]);
        }
    }
        
}

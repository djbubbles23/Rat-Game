using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public int damage = 5;

    private Transform player;
    private PlayerStats playerStats;
    private void Start()
    {
        print("BULLET");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        
        Vector3 playerDirection = player.position - transform.position;
        this.GetComponent<Rigidbody>().AddForce(playerDirection, ForceMode.Impulse);

        StartCoroutine(DeathTimer(10)); // despawn bullet after 10 seconds
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerStats.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }

    private IEnumerator DeathTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}

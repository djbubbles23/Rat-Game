using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public ParticleSystem bloodParticles;
    public float maxHealth = 100f;
    public float damage = 10f;
    public float knockback = 10f;
    public float upwardKnockback = 10f;

    private Rigidbody rb;
    private NavMeshAgent agent;
    private float health;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
    }

    public void Attack()
    {
        
    }

    public void TakeDamage(float damage)
    {
        // make blood splatter;
        ParticleSystem blood = Instantiate(bloodParticles, transform.position, Quaternion.identity);
        blood.transform.SetParent(this.transform);
        
        // handle health stats
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeKnockback(Vector3 direction)
    {
        rb.AddForce(Vector3.up * upwardKnockback, ForceMode.Impulse);
        rb.AddForce(direction * knockback, ForceMode.Impulse);
    }
    
}

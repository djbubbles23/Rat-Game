using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class EnemyBehavior : MonoBehaviour
{
    public ParticleSystem bloodParticles;       // Blood particles that play when enemy is damaged
    private VisualEffect atc;                   // VisualEffect component that creates the enemy's attacks
    public float maxHealth = 100f;              // Starting health of the enemy
    public float damage = 10f;                  // How powerful enemy's attack is
    public float knockback = 10f;               // How far back an enemy flies when hit
    public float upwardKnockback = 10f;         // How far up an enemy moves when hit

    public float attackDelay = 2.0f;            // time between attack

    private Rigidbody rb;                       // Enemy rigidbody
    private float health;                       // Current health

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
    }

    public void Attack()
    {
        atc.Play();
    }

    public void TakeDamage(float damage)
    {
        // make blood splatter;
        Vector3 bloodPosition = transform.position;
        bloodPosition.y -= 0.2f;    //lower blood to rat
        ParticleSystem blood = Instantiate(bloodParticles, bloodPosition, Quaternion.identity);
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

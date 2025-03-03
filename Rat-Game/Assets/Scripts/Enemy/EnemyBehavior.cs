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

    public float attackDelay = 5.0f;            // time between attack
    public float attackLength = 0.2f;           // how long the attack hb is active

    private Rigidbody rb;                       // Enemy rigidbody
    private Collider attackCollider;            // the attack hb attached to the swipe
    private float health;                       // Current health
    private float attackTimer;                  // current time until next attack
    private bool canAttack;

    private void Start()
    {
        // initialize variables
        rb = GetComponent<Rigidbody>();
        atc = gameObject.GetComponentInChildren<VisualEffect>();
        attackCollider = GetComponentInChildren<BoxCollider>();
        health = maxHealth;
    }

    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            atc.Play();
            
            // activate attack hitbox
            StartCoroutine(ActivateAttackCollider());
        }
        
    }

    private void Update()
    {
        // keep track of attack cooldown
        if (!canAttack)
        {
            attackTimer -= Time.deltaTime;
        }

        // reset attack
        if (attackTimer <= 0)
        {
            canAttack = true;
            attackTimer = attackDelay;
        }
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

    IEnumerator ActivateAttackCollider()
    {
        attackCollider.enabled = true;
        yield return new WaitForSeconds(attackLength);
        attackCollider.enabled = false;
    }
    
    // damage player if attack connects
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            player.TakeDamage(damage);
            
            Debug.Log("Dealt " + damage + " damage to player");
            
            // calculate knockback direction
            //Vector3 knockbackDirection = other.transform.position - transform.position;
            //player.TakeKnockback(knockbackDirection);
        }
    }
}

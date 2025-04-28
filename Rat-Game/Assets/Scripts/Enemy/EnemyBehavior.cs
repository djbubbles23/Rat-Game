using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject bloodPrefab;              // Blood particles that play when enemy is damaged
    public Transform bloodSpawnPoint;           // Where the blood should spawn from
    private VisualEffect atc;                   // VisualEffect component that creates the enemy's attacks
    public AudioClip takeDamageSound;           // sound to play when hurt
    public Animator animator;
    
    public float maxHealth = 100f;              // Starting health of the enemy
    public float damage = 10f;                  // How powerful enemy's attack is

    public float attackDelay = 5.0f;            // time between attack
    public float attackLength = 0.2f;           // how long the attack hb is active
    public float flashDuration = 2f;            // how long damage flash should last

    private Rigidbody rb;                       // Enemy rigidbody
    private Collider attackCollider;            // the attack hb attached to the swipe
    private AudioSource audioSource;            // enemy audio source
    
    private float health;                       // Current health
    private float attackTimer;                  // current time until next attack
    private bool canAttack = false;
    private bool attacking = false;

    private PlayerStats playerScore;            // player object
    public GameObject ratGeo;                   // rat geometry gameobject
    private MeshRenderer renderer;
    private Color originalColor;


    private void Start()
    {
        // initialize variables
        atc = gameObject.GetComponentInChildren<VisualEffect>();
        attackCollider = GetComponentInChildren<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        health = maxHealth;
        renderer = ratGeo.GetComponent<MeshRenderer>();
        originalColor = renderer.material.color;    

        // find the player object and get the PlayerStats component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScore = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    public void Attack()
    {
        if(!attacking)
            StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        attacking = true;

        // Start wind-up / delay
        yield return new WaitForSeconds(attackDelay);

        // Play animation
        animator.SetTrigger("Attack");
        atc.Play();

        // Activate collider halfway through
        yield return new WaitForSeconds(1.18f);
        attackCollider.enabled = true;

        yield return new WaitForSeconds(attackLength);
        attackCollider.enabled = false;

        // Cooldown or reset (if needed, add a delay here)
        attacking = false;
    }
    
    public void ResetAttack()
    {
        canAttack = false;
        attacking = false;
    }
    
    public void TakeDamage(float damage)
    {
        // create blood VFX
        GameObject blood = Instantiate(bloodPrefab, bloodSpawnPoint.position, bloodSpawnPoint.rotation);
        VisualEffect vfx = blood.GetComponentInChildren<VisualEffect>();
        vfx.Play();
        StartCoroutine(DestroyVFXAfterTime(blood, 2.0f));
            
        // flash rat red
        StartCoroutine(DamageFlash(flashDuration));
        
        // handle health stats
        health -= damage;
        
        // play sfx
        audioSource.pitch = Random.Range(0.8f, 2.0f);
        audioSource.PlayOneShot(takeDamageSound);
        
        // play animation
        if (animator != null)
        {
            animator.SetTrigger("Damage");
        }

        if (health <= 0)
        {
            playerScore.score += 100;
            Debug.Log("Enemy killed! Score: " + playerScore.score);
            Destroy(gameObject);
            
            // play animation
            if (animator != null)
            {
                animator.SetTrigger("Death");
            }
        }
    }
    
    IEnumerator DamageFlash(float duration)
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(duration);
        renderer.material.color = originalColor;
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
    
    public bool IsAttacking()
    {
        return attacking;
    }
    
    // destory VFX effect i.e. blood
    private IEnumerator DestroyVFXAfterTime(GameObject vfx, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(vfx);
    }
}

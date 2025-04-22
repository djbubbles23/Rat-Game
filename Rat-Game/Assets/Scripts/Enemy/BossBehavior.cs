using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class BossBehavior : MonoBehaviour
{
    private VisualEffect atc;                   // VisualEffect component that creates the enemy's attacks
    public AudioClip takeDamageSound;           // sound to play when hurt
    public CapsuleCollider hitbox;
    public Animator animator;
    
    public Collider attackCollider;             // the attack hb attached to the swipe
    public Collider slamCollider;               // the attack hb attached to the slam
    public GameObject ratGeo;                   // rat geometry gameobject
    
    public float maxHealth = 100f;              // Starting health of the enemy
    public float damage = 10f;                  // How powerful enemy's attack is

    public float attackDelay = 5.0f;            // time between attack/ start-up time
    public float jumpHeight = 5.0f;             // height of slam attack animation
    public float jumpDuration = 3.5f;           // length of jump duration
    public float attackLength = 0.2f;           // how long the attack hb is active
    public float flashDuration = 2f;            // how long damage flash should last

    private AudioSource audioSource;            // enemy audio source
    private NavMeshAgent agent;                 // enemy ai agent
    
    private float health;                       // Current health
    private float nextAttackTime;               // time until next attack
    private bool canAttack = false;
    private bool attacking = false;

    private PlayerStats playerScore;            // player object
    private List<SkinnedMeshRenderer> skinnedRenderers = new List<SkinnedMeshRenderer>();
    private List<Color> originalColors = new List<Color>();

    private void Start()
    {
        // initialize variables
        atc = gameObject.GetComponentInChildren<VisualEffect>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
        
        // Get all SkinnedMeshRenderers in children of ratGeo
        skinnedRenderers.AddRange(ratGeo.GetComponentsInChildren<SkinnedMeshRenderer>());

        foreach (var renderer in skinnedRenderers)
        {
            originalColors.Add(renderer.material.color);
        }

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
        if(!attacking && Time.time >= nextAttackTime)
            StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        attacking = true;
        nextAttackTime = Time.time + attackDelay;
        
        // Stop movement
        agent.isStopped = true;

        // Start wind-up / delay
        yield return new WaitForSeconds(attackDelay);

        // Play animation
        animator.SetTrigger("Attack");

        // Activate collider halfway through
        yield return new WaitForSeconds(1.18f);
        atc.Play();
        attackCollider.enabled = true;

        yield return new WaitForSeconds(attackLength);
        attackCollider.enabled = false;
        
        // Resume movement
        agent.isStopped = false;

        // Cooldown or reset (if needed, add a delay here)
        attacking = false;
    }

    public void Slam()
    {
        if(!attacking) 
            StartCoroutine(DoSlam());
    }

    IEnumerator DoSlam()
    {
        if (attacking) yield break;
        print("start SLAM");
        attacking = true;

        StartCoroutine(SlamAnimation());
        yield return new WaitForSeconds(attackLength);
        attacking = false;
    }

    IEnumerator SlamAnimation()
    {
        hitbox.enabled = false; // disable enemy hb
        animator.SetTrigger("Slam");
        
        // jump arc
        float duration = 3.5f;
        float time  = 0.0f;
        
        Vector3 originalPos = ratGeo.transform.localPosition;
        bool activated = false;

        while (time < duration)
        {
            float yOffset = Mathf.Sin((time / jumpDuration) * Mathf.PI) * jumpHeight;
            ratGeo.transform.localPosition = new Vector3(originalPos.x, originalPos.y + yOffset, originalPos.z);

            if (!activated && time >= 1.7f)
            {
                StartCoroutine(ActivateAttackCollider(slamCollider));
                activated = true;
            }
            
            time += Time.deltaTime;
            yield return null;
        }
        
        hitbox.enabled = true; // re-enable enemy hb
    }
    
    public void ResetAttack()
    {
        canAttack = false;
        attacking = false;
    }

    IEnumerator ActivateAttackCollider(Collider attack)
    {
        attack.enabled = true;
        yield return new WaitForSeconds(attackLength);
        attack.enabled = false;
    }
    
    public void TakeDamage(float damage)
    {
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
        for (int i = 0; i < skinnedRenderers.Count; i++)
        {
            skinnedRenderers[i].material.color = Color.red;
        }

        yield return new WaitForSeconds(duration);

        for (int i = 0; i < skinnedRenderers.Count; i++)
        {
            skinnedRenderers[i].material.color = originalColors[i];
        }
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

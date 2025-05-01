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
    public AudioClip swipeSound;                // sound to play when enemy attack
    public Animator animator;

    public float maxHealth = 100f;              // Starting health of the enemy
    public float damage = 10f;                  // How powerful enemy's attack is

    public float attackDelay = 5.0f;            // time between attack
    public float attackLength = 0.2f;           // how long the attack hb is active
    public float flashDuration = 2f;            // how long damage flash should last

    private Rigidbody rb;                       // Enemy rigidbody
    private Collider attackCollider;            // the attack hb attached to the swipe
    private AudioSource audioSource;            // enemy audio source
    private NavMeshAgent agent;                 // enemy ai agent

    private float health;                       // Current health
    private float nextAttackTime;               // time until next attack
    private bool canAttack = false;
    private bool attacking = false;

    private PlayerStats playerScore;            // player object

    public GameObject ratGeo;                   // rat geometry gameobject
    private MeshRenderer renderer;
    private List<SkinnedMeshRenderer> skinnedRenderers = new List<SkinnedMeshRenderer>();
    private List<Color> originalColors = new List<Color>();


    private void Start()
    {
        // initialize variables
        atc = gameObject.GetComponentInChildren<VisualEffect>();
        attackCollider = GetComponentInChildren<BoxCollider>();
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
        if (!attacking && Time.time >= nextAttackTime)
            StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        attacking = true;
        nextAttackTime = Time.time + attackDelay;

        // Stop movement
        agent.isStopped = true;

        // Play animation
        animator.SetTrigger("Attack");

        // Activate collider halfway through
        yield return new WaitForSeconds(1.18f);
        atc.Play();
        audioSource.PlayOneShot(swipeSound);
        attackCollider.enabled = true;

        yield return new WaitForSeconds(1.2f);
        attackCollider.enabled = false;

        // Resume movement
        agent.isStopped = false;

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
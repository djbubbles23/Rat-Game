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
    
    public float maxHealth = 100f;              // Starting health of the enemy
    public float damage = 10f;                  // How powerful enemy's attack is

    public float attackDelay = 5.0f;            // time between attack/ start-up time
    public float slamStartUp = 5.0f;
    public float attackLength = 0.2f;           // how long the attack hb is active
    public float flashDuration = 2f;            // how long damage flash should last

    public Collider attackCollider;            // the attack hb attached to the swipe
    public Collider slamCollider;              // the attack hb attached to the slam
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
        if(!attacking) StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        if (attacking) yield break;
        attacking = true;
        
        yield return new WaitForSeconds(attackDelay);
        
        atc.Play();
            
        // activate attack hitbox
        StartCoroutine(ActivateAttackCollider(attackCollider));
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
        yield return new WaitForSeconds(attackLength + 1f);
        attacking = false;
    }

    IEnumerator SlamAnimation()
    {
        hitbox.enabled = false; // disable enemy hb
        
        // jump arc
        float height = 60.0f;
        float duration = 3.5f;
        float time  = 0.0f;
        
        Vector3 originalPos = ratGeo.transform.localPosition;

        while (time < duration)
        {
            float yOffset = Mathf.Sin((time / duration) * Mathf.PI) * height;
            ratGeo.transform.localPosition = new Vector3(originalPos.x, originalPos.y + yOffset, originalPos.z);
            time += Time.deltaTime;
            yield return null;
        }
        
        hitbox.enabled = true; // re-enable enemy hb
        StartCoroutine(ActivateAttackCollider(slamCollider));
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

}

using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;      // max health
    
    public AudioClip takeDamageSound;
    public AudioClip deathSound;
    
    private AudioSource audioSource;     // player audiosource
    private float currentHealth;        // current health of the player

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        AudioClip clip = takeDamageSound;

        if (currentHealth <= 0)
        {
            Debug.Log("PLAYER DIED!");
            clip = deathSound;
        }
        
        audioSource.PlayOneShot(clip);
    }
}

using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;      // max health
    
    private float currentHealth;        // current health of the player

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("PLAYER DIED!");
        }
    }
}

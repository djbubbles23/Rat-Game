using System;
using UnityEngine;
// Removed conflicting alias

public class CheckCollision : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                enemy.TakeDamage(playerStats.damage);
            }
            
            // calculate knockback direction
            Vector3 knockbackDirection = other.transform.position - transform.position;
            enemy.TakeKnockback(knockbackDirection);
        }
    }

    /*
    private int calculateDamage(diceScriptableObject dice1Values, diceScriptableObject dice2Values, diceScriptableObject dice3Values)
    {
        int totalDamage = 0;
        

    }
    */
}

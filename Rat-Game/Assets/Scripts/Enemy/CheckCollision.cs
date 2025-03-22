using System;
using UnityEngine;
using playerDiceInv = playerDiceInv;

public class CheckCollision : MonoBehaviour
{
    public float damage = 10f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            enemy.TakeDamage(damage);
            
            // calculate knockback direction
            Vector3 knockbackDirection = other.transform.position - transform.position;
            enemy.TakeKnockback(knockbackDirection);
        }
    }
    
}

using System;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public weaponController weaponController; // Reference to the weaponController script

    private void Start()
    {
        // Ensure the weaponController is assigned
        if (weaponController == null)
        {
            weaponController = GetComponentInParent<weaponController>();
            if (weaponController == null)
            {
                Debug.LogError("weaponController script not found!");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            enemy.TakeDamage(weaponController.calculateDmg());
            Debug.Log("Hit enemy: " + other.name + " with damage: " + weaponController.calculateDmg());
            
            // calculate knockback direction
            //Vector3 knockbackDirection = other.transform.position - transform.position;
            //enemy.TakeKnockback(knockbackDirection);
        }
    }
    
}

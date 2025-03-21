using UnityEngine;
using System.Collections;

public class DamageZone : MonoBehaviour
{
    public float damagePerSecond = 1f; // Damage amount
    private bool playerInZone = false; // Check if player is in the zone
    private PlayerStats playerStats; // Reference to PlayerStats

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerInZone = true;
                StartCoroutine(DealDamageOverTime());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (playerInZone && playerStats != null)
        {
            playerStats.TakeDamage(damagePerSecond);
            yield return new WaitForSeconds(1f); // Wait for 1 second before dealing damage again
        }
    }
}

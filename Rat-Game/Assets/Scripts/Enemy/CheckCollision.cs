using System;
using UnityEngine;
using TMPro;

public class CheckCollision : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public GameObject attcHitbox;
    private string popupText = "Hit";

    private EnemyBehavior enemy;
    private weaponController weaponController;

    private void OnTriggerEnter(Collider other)
    {
        enemy = other.GetComponent<EnemyBehavior>();
        weaponController = GetComponent<weaponController>();

        if (other.tag == "Enemy")
        {
            int damage = weaponController.calculateDmg();
            enemy.TakeDamage(damage);

            // Calculate knockback direction
            Vector3 knockbackDirection = other.transform.position - transform.position;
            enemy.TakeKnockback(knockbackDirection);

            // Show floating text on the enemy
            if (floatingTextPrefab)
            {
                showFloatingText(damage, other.transform.position);
            }
        }
    }

    public void showFloatingText(int damage, Vector3 enemyPosition)
    {
        popupText = damage.ToString();
        Vector3 popupPosition = enemyPosition + new Vector3(0, 1, 0); // Offset above the enemy
        GameObject floatingText = Instantiate(floatingTextPrefab, popupPosition, Quaternion.identity);
        Debug.Log("Floating text instantiated");
        floatingText.GetComponent<TextMeshPro>().text = popupText;
    }
}

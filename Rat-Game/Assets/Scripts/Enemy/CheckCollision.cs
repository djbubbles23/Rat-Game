using System;
using UnityEngine;
using TMPro;

public class CheckCollision : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public GameObject attcHitbox;
    private string popupText = "Hit";

    private EnemyBehavior enemy;
    public weaponController weaponController;

    private void OnTriggerEnter(Collider other)
{
    enemy = other.GetComponent<EnemyBehavior>();
    //weaponController = GetComponent<weaponController>();

    if (other.CompareTag("Enemy") && enemy != null && weaponController != null)
    {
        int damage = weaponController.calculateDmg();
        enemy.TakeDamage(damage);

        Vector3 knockbackDirection = other.transform.position - transform.position;
        enemy.TakeKnockback(knockbackDirection);

        if (floatingTextPrefab)
        {
            Debug.Log("Floating text prefab is assigned");
            showFloatingText(damage, other.transform.position);
        }
        else
        {
            Debug.LogError("floatingTextPrefab is not assigned!");
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

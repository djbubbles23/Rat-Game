using System;
using UnityEngine;
using TMPro;
public class CheckCollision : MonoBehaviour
{
    public weaponController weaponController; // Reference to the weaponController script
    public GameObject floatingTextPrefab; 
    private int damage = 0;

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
            damage = weaponController.calculateDmg();
            enemy.TakeDamage(damage);
            showFloatingText(other.transform.position, damage);
            Debug.Log("Hit enemy: " + other.name + " with damage: " + damage);
        }
        if (other.tag == "Boss")
        {
            BossBehavior enemy = other.GetComponent<BossBehavior>();
            damage = weaponController.calculateDmg();
            enemy.TakeDamage(damage);
            showFloatingText(other.transform.position, damage);
            Debug.Log("Hit enemy: " + other.name + " with damage: " + damage);
        }
    }

    private void showFloatingText(Vector3 enemy, int damage)
    {
        string popupText = damage.ToString();
        GameObject floatingText = Instantiate(floatingTextPrefab, enemy, Quaternion.identity);
        floatingText.GetComponent<TextMeshPro>().text = popupText;
        floatingText.transform.position = enemy + new Vector3(1f, 1f, 0); 
        Debug.Log("Floating text instantiated: " + popupText);
    }
}

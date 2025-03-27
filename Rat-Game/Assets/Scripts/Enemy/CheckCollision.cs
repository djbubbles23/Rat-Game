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

    void Update()
    {
        // Im so so so so so so so sorry for hard coding this but I need to get this done for the demo
        if(weaponController != null){
            //Debug.Log("WeaponController is not null");
            string weaponName = weaponController.weapon.weaponObj.name;
            //Debug.Log("Weapon name: " + weaponName);
            if(weaponName == "SwordOBJ"){
                attcHitbox.transform.localScale = new Vector3(2.5f, 0.5f, 2f);
                Debug.Log("Sword hitbox size set to: " + attcHitbox.transform.localScale);
            }
            else if(weaponName == "LongSwordOBJ"){
                attcHitbox.transform.localScale = new Vector3(3.5f, 0.5f, 2.5f);
                Debug.Log("LongSword hitbox size set to: " + attcHitbox.transform.localScale);
            }
            else if(weaponName == "DaggerOBJ"){
                attcHitbox.transform.localScale = new Vector3(0.5f, 0.5f, 1.5f);
                Debug.Log("Dagger hitbox size set to: " + attcHitbox.transform.localScale);
            }  
            else {
                Debug.Log("Weapon name not recognized: " + weaponName);
            }
        }      
    }

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

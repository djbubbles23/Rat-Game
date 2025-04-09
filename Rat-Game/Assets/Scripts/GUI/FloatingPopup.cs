using UnityEngine;
using TMPro;

public class FloatingPopup : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public GameObject attcHitbox;
    public weaponController weaponController; 
    private string popupText = "";

    void Start()
    {
        weaponController = GetComponentInParent<weaponController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")){
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            showFloatingText(enemy.transform.position);
        }
    }
    void showFloatingText(Vector3 enemy)
    {
        Vector3 popupPosition = enemy + new Vector3(0, 0, -1); 
        GameObject floatingText = Instantiate(floatingTextPrefab, popupPosition, Quaternion.identity, transform);
        floatingText.GetComponent<TextMeshPro>().text = weaponController.calculateDmg().ToString(); 
        Debug.Log("Floating text instantiated: " + popupText);
    }
}

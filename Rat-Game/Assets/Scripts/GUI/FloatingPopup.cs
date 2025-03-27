using UnityEngine;
using TMPro;

public class FloatingPopup : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public GameObject Victim;
    public GameObject attcHitbox;
    public string popupText = "Hit";
    

    
    public void OnTriggerEnter(Collider other)
    {
        if(floatingTextPrefab && other.gameObject == attcHitbox){
            showFloatingText();
        }
    }
    void showFloatingText()
    {
        Vector3 popupPosition = transform.position + new Vector3(0, 0, -1); 
        GameObject floatingText = Instantiate(floatingTextPrefab, popupPosition, Quaternion.identity, transform);
        floatingText.GetComponent<TextMeshPro>().text = popupText;
    }
}

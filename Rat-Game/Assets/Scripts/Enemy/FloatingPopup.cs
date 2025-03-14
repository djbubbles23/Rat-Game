using UnityEngine;

public class FloatingPopup : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public GameObject Victum;
    public float scale = 1.0f;
    
    public void OnCollisionEnter(Collision collision)
    {
        if(floatingTextPrefab){
            showFloatingText();
        }
    }
    void showFloatingText()
    {
        Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
    }
}

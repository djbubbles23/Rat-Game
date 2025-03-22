using UnityEngine;

public class weaponController : MonoBehaviour
{
    public weaponScriptableObject weapon;
    private GameObject weaponInstance;
    public Vector3 weaponScale = Vector3.one;
    void Start()
    {
        // Check if the weapon object is valid
        if (weapon.weaponObj is GameObject)
        {
            // Instantiate the weapon in the scene
            weaponInstance = Instantiate(weapon.weaponObj);
            weaponInstance.transform.SetParent(transform);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localScale = weaponScale;
        }
        else
        {
            Debug.LogError("weaponObj is not a valid");
        }
    }
}

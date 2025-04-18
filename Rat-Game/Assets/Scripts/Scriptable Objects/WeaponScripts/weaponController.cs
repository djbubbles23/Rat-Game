using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class weaponController : MonoBehaviour
{
    public weaponScriptableObject weapon;
    private GameObject weaponInstance;
    public Vector3 weaponScale = Vector3.one;

    // On weapon equip
    /*
    public diceScriptableObject diceSlot1;
    public diceScriptableObject diceSlot2;
    public diceScriptableObject diceSlot3;
    */
    public diceScriptableObject[] diceSlots = new diceScriptableObject[3];

    public INVManager invManager;

    public bool debug = false;
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

    void Update()
    {
    } 

    public int calculateDmg(){
        int totalDamage = 0;
        for(int i=0; i<diceSlots.Length; i++){
            if(diceSlots[i] != null){
                totalDamage += diceSlots[i].diceValue[Random.Range(0, diceSlots[i].diceValue.Length)];
            }
        }
        Debug.Log("Total Damage: " + totalDamage);
        return totalDamage;
    }
}

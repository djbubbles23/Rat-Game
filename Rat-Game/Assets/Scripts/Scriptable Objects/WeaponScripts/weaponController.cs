using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
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

    void Start()
    {
    }

    void Update()
    {
        // Sync weapon in the weaponSlot with weaponController
        weaponEquip(weapon);
    } 

    public int calculateDmg(){

        //dice base damage
        //need to change dice slots based on tier
        int totalDamage = 0;
        int weaponTeir = 0;
        if(weapon.weaponObj.gameObject.name == "DaggerOBJ"){
            weaponTeir = 1;
        }
        else if(weapon.weaponObj.gameObject.name == "SwordOBJ"){
            weaponTeir = 2;
        }
        else if(weapon.weaponObj.gameObject.name == "LongSwordOBJ"){
            weaponTeir = 3;
        }
        //based on weapon tier, only use those dice slots
        //for example, if weapon tier is 2, only use dice slot 1 and 2

        //get slot 1
        if(diceSlots[0] != null && weaponTeir >= 1){
            totalDamage += diceSlots[0].diceValue[Random.Range(0, diceSlots[0].diceValue.Length)];
        }
        //get slot 2
        if(diceSlots[1] != null && weaponTeir >= 2){
            totalDamage += diceSlots[1].diceValue[Random.Range(0, diceSlots[1].diceValue.Length)];
        }
        //get slot 3
        if(diceSlots[2] != null && weaponTeir >= 3){
            totalDamage += diceSlots[2].diceValue[Random.Range(0, diceSlots[2].diceValue.Length)];
        }
        
        //weapon damage amplifier
        int weaponDamgeAmp = 0;
        if(weapon.weaponObj.gameObject.name == "DaggerOBJ"){
            weaponDamgeAmp = 1;
        }
        else if(weapon.weaponObj.gameObject.name == "SwordOBJ"){
            weaponDamgeAmp = 2;
        }
        else if(weapon.weaponObj.gameObject.name == "LongSwordOBJ"){
            weaponDamgeAmp = 3;
        }

        Debug.Log("Total Damage: " + totalDamage);
        return totalDamage*weaponDamgeAmp;
    }

    public void weaponEquip(weaponScriptableObject newWeapon){
        // Destroy the previous weapon instance if it exists
        if (weaponInstance != null)
        {
            Destroy(weaponInstance);
        }

        weaponInstance = Instantiate(newWeapon.weaponObj);
        weaponInstance.transform.SetParent(transform);
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localScale = weaponScale;
        weaponInstance.transform.localRotation = Quaternion.identity;

        /*
        if(newWeapon.weaponObj.gameObject.name == "DaggerOBJ"){
            weaponInstance.transform.localRotation = Quaternion.Euler(90, 90, 90);
        }
        */
    }
}

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
        for(int i=0; i<diceSlots.Length; i++){
            if(diceSlots[i] != null){
                totalDamage += diceSlots[i].diceValue[Random.Range(0, diceSlots[i].diceValue.Length)];
            }
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

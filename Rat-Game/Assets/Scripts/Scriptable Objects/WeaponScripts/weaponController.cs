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

    void Start()
    {
        weaponEquip(weapon);
    }

    void Update()
    {
        // Sync weapon in the weaponSlot with weaponController
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

    public void weaponEquip(weaponScriptableObject newWeapon){

        if (newWeapon.weaponObj is GameObject)
        {
            weaponInstance = Instantiate(newWeapon.weaponObj);
            weaponInstance.transform.SetParent(transform);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localScale = weaponScale;
        }
    }
}

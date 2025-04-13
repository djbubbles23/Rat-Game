using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class weaponController : MonoBehaviour
{
    public weaponScriptableObject weapon;
    private GameObject weaponInstance;
    public Vector3 weaponScale = Vector3.one;

    // On weapon equip
    public diceScriptableObject diceSlot1;
    public diceScriptableObject diceSlot2;
    public diceScriptableObject diceSlot3;

    public diceScriptableObject[] diceSlots = new diceScriptableObject[3];

    public INVManager invManager;

    public bool debug = false;
    void Start()
    {
        if(debug){
            //debugging dice slots
            if(diceSlot1==null){
                Debug.Log("Dice 1 is null");
            }
            else{
                Debug.Log("Dice 1 is equiped");
                diceSlot1.displayAll();
            }
            if(diceSlot2==null){
                Debug.Log("Dice 2 is null");
            }
            else{
                Debug.Log("Dice 2 is equiped");
                diceSlot2.displayAll();
            }        
            if(diceSlot3==null){
                Debug.Log("Dice 3 is null");
            }
            else{
                Debug.Log("Dice 3 is equiped");
                diceSlot3.displayAll();
            }

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
        invManager = GameObject.Find("InventoryManager").GetComponent<INVManager>();

    }

    void Update()
    {
        /*
        for(int i = 0; i < diceSlots.Length; i++){
            invManager.Eslots[i].GetComponent<diceScriptableObject>() = diceSlots[i];
        }*/
    } 

    public int calculateDmg(){
        int totalDamage = 0;
        if (diceSlot1 != null) {
            totalDamage += diceSlot1.diceValue[Random.Range(0, diceSlot1.diceValue.Length)];
        }
        if (diceSlot2 != null) {
            totalDamage += diceSlot2.diceValue[Random.Range(0, diceSlot2.diceValue.Length)];
        }
        if (diceSlot3 != null) {
            totalDamage += diceSlot3.diceValue[Random.Range(0, diceSlot3.diceValue.Length)];
        }
        Debug.Log("Total Damage: " + totalDamage);
        return totalDamage;
    }
}

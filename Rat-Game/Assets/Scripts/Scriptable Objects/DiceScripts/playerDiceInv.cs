using UnityEngine;

public class playerDiceInv : MonoBehaviour
{
    // On weapon equip
    public diceScriptableObject diceSlot1;
    public diceScriptableObject diceSlot2;
    public diceScriptableObject diceSlot3;

    // On inventory equip
    public diceScriptableObject invSlot1;
    public diceScriptableObject invSlot2;
    public diceScriptableObject invSlot3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }
}

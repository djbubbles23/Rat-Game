using UnityEngine;

public class playerDiceInv : MonoBehaviour
{
    public GameObject diceSlot1;
    public GameObject diceSlot2;
    public GameObject diceSlot3;
    public GameObject invSlot1;
    public GameObject invSlot2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(diceSlot1==null){
            Debug.Log("Dice 1 is null");
        }
        else{
            Debug.Log("Dice 1 is equiped");
            diceSlot1.GetComponent<Dice>().displayAll();
        }
        if(diceSlot2==null){
            Debug.Log("Dice 2 is null");
        }
        else{
            Debug.Log("Dice 2 is equiped");
        }        
        if(diceSlot3==null){
            Debug.Log("Dice 3 is null");
        }
        else{
            Debug.Log("Dice 3 is equiped");
        }
    }
}

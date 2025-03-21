using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    public diceScriptableObject dice;
    private int diceTypeTemp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //keep them in the same order because cost is dependant on values and values are dependant on type
        dice.diceType = createDiceType();
        diceTypeTemp = dice.diceType;
        dice.diceValue = createDiceValues();
        dice.diceCost = createDiceCost();
    }

    // show dice data
    public void displayAll(){
        showDiceType();
        showDiceValues();
        showDiceCost();
    }

    public void showDiceType()
    {
        Debug.Log("Dice Type: d"+dice.diceType);
    }

    public void showDiceCost()
    {
        Debug.Log("Dice Cost: $"+dice.diceCost);
    }
    public void showDiceValues()
    {
        if (dice.diceValue != null)
        {
            for (int i = 0; i < dice.diceValue.Length; i++)
            {
                Debug.Log($"dice.diceValue[{i}] = {dice.diceValue[i]}");
            }
        }
    }

    //create dice data
    public int createDiceType(){
        int diceTypeRand = Random.Range(1,4);
        switch(diceTypeRand){
            case 1:
                return dice.diceType = 4; //4 sided dice
            case 2:
                return dice.diceCost = 6; //6 sided dice
            case 3:
                return dice.diceCost = 8; //8 sided dice
            default:
                return dice.diceCost = 0; //0 sided dice
        }
    }

public int[] createDiceValues() {
    dice.diceValue = new int[dice.diceType]; // Array size matches diceType
    Debug.Log("Dice Type createdicevalues: d" + dice.diceType);
    for (int i = 0; i < dice.diceType; i++) { // Loop runs from 0 to diceType - 1
        dice.diceValue[i] = Random.Range(0, dice.diceType);
    }
    return dice.diceValue;
}

    public int createDiceCost()
    {
        int totalCost = 0;
        if (dice.diceValue != null && dice.diceValue.Length > 0)
        {
            foreach (int diceValue in dice.diceValue)
            {
                totalCost += diceValue;
            }
        }
        //avg of values
        return totalCost/dice.diceType;
    }

}

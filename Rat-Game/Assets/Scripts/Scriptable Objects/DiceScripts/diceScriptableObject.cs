using System;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Dice")]
public class diceScriptableObject : ScriptableObject
{
    public string diceName;
    public string diceDescription;
    //public effectValue
    public int diceType;
    public int[] diceValue;
    public float diceCost;

    private void OnEnable()
    {
        //keep them in the same order because cost is dependant on values and values are dependant on type
        diceType = createDiceType();
        diceValue = createDiceValues();
        diceCost = createDiceCost();
    }

    
    // show dice data
    public void displayAll(){
        showDiceType();
        showDiceValues();
        showDiceCost();
    }

    public void showDiceType()
    {
        Debug.Log("Dice Type: d"+diceType);
    }

    public void showDiceCost()
    {
        Debug.Log("Dice Cost: $"+diceCost);
    }
    public void showDiceValues()
    {
        if (diceValue != null)
        {
            for (int i = 0; i < diceValue.Length; i++)
            {
                Debug.Log($"dice.diceValue[{i}] = {diceValue[i]}");
            }
        }
    }

    //create dice data
    public int createDiceType(){
        int diceTypeRand = Random.Range(1,4);
        switch(diceTypeRand){
            case 1:
                return diceType = 4; //4 sided dice
            case 2:
                return diceType = 6; //6 sided dice
            case 3:
                return diceType = 8; //8 sided dice
            default:
                return diceType = 0; //0 sided dice
        }
    }

    public int[] createDiceValues() {
        diceValue = new int[diceType]; // Array size matches diceType
        //Debug.Log("Dice Type createdicevalues: d" + diceType);
        for (int i = 0; i < diceType; i++) { // Loop runs from 0 to diceType - 1
            diceValue[i] = Random.Range(0, diceType);
        }
        return diceValue;
    }

    public float createDiceCost()
    {
        float totalCost = 0f;
        if (diceValue != null && diceValue.Length > 0)
        {
            foreach (int diceValue in diceValue)
            {
                totalCost += diceValue;
            }
        }
        //avg of values
        return (totalCost/diceValue.Length)*10;
    }

}

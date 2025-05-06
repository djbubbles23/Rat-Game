using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEditor;


[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Dice")]
public class diceScriptableObject : ScriptableObject
{   
    [Header("Dice Data")]
    [Header("Create dice type first (4, 6, or 8)")]
    public int diceType;
    public string diceName;
    public string diceDescription;
    public string effectValue;
    public int[] diceValue;
    public float diceCost;
    public Sprite icon; 

    private void OnEnable()
    {
        //keep them in the same order because cost is dependant on values and values are dependant on type
        //diceType = createDiceType();
        diceValue = createDiceValues();
        diceCost = createDiceCost();

        diceDescription = createDiceDescription();
        diceName = "D" + diceType;

        effectValue = createEffectValue();

        icon = getIcon(diceType);
        Debug.Log("Dice Created: " + diceName);
    }

    
    // show dice data
    public void displayAll(){
        showDiceType();
        showDiceValues();
        showDiceCost();
        showDiceEffect();
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
    public void showDiceEffect(){
        Debug.Log("Dice Effect: "+ effectValue);
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
            diceValue[i] = Random.Range(1, diceType+1);
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

    public string createDiceDescription()
    {
        diceDescription = "This is a d"+diceType+". There is a 1/" + diceType + " to get one of these values: ";
        for (int i = 0; i < diceValue.Length; i++)
        {
            diceDescription += diceValue[i];
            if (i < diceValue.Length - 1)
            {
                diceDescription += ", ";
            }
        }
        diceDescription += ".";
        return diceDescription;
    }

    public string createEffectValue()
    {
        int effectValueRand = Random.Range(0,10);
        if(effectValueRand == 0){
            return "Ice";
        }
        else if(effectValueRand == 1){
            return "Fire";
        }
        else if(effectValueRand == 2){
            return "Toxic";
        }
        else{
            return "None";
        }
    }

    public Sprite getIcon(int diceType)
    {
        string path = "";
        switch (diceType)
        {
            case 4:
                path = "Images/d4_icon";
                break;
            case 6:
                path = "Images/d6_icon";
                break;
            case 8:
                path = "Images/d8_icon";
                break;
            default:
                Debug.LogError("Invalid dice type: " + diceType);
                return null;
        }

        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite == null)
        {
            Debug.LogError("Sprite not found at path: " + path);
        }
        return sprite;
    }
    #if UNITY_EDITOR
    [ContextMenu("Regenerate Dice")]
    public void Regenerate()
    {
        OnEnable();
        EditorUtility.SetDirty(this);
    }
    #endif


}



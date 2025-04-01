using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Dice")]
public class diceScriptableObject : ScriptableObject
{
    public int[] diceValue;
    public int diceType;
    public int diceCost;
    
    public int getDiceType()
    {
        return diceType;
    }

    public int getDiceCost()
    {
        return diceCost;
    }
}

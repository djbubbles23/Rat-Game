using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Dice")]
public class diceScriptableObject : ScriptableObject
{
    public int diceType;
    public int[] diceValue;
    public int diceCost;

}

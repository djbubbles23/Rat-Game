using UnityEngine;

public class diceRandom : MonoBehaviour
{
    public diceScriptableObject dice;
    public int diceValue;
    public int diceType;
    public int diceCost;

    void Start()
    {
        diceType = dice.getDiceType();
        diceCost = dice.getDiceCost();
        diceValue = dice.diceValue[Random.Range(0, diceType)];
    }
}

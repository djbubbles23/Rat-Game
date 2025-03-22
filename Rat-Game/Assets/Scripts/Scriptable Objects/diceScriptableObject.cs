using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Dice")]
public class diceScriptableObject : ScriptableObject
{
    public string diceName;
    public string diceDescription;
    public Sprite diceImage;
    //public effectValue
    public int diceType;
    public int[] diceValue;
    public int diceCost;

}

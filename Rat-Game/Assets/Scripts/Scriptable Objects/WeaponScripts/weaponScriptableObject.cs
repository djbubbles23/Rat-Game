using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Weapon")]
public class weaponScriptableObject : ScriptableObject
{
    public string weaponName;
    public string weaponDescription;
    public GameObject weaponObj;
    public int tier;
    //public int weaponCost;

}

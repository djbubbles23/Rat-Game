using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Weapon")]
public class weaponScriptableObject : ScriptableObject
{
    public Sprite icon;
    public string weaponName;
    public string weaponDescription;
    public GameObject weaponObj;
    public int tier;
    public float weaponCost;

    private void onEnable(){
        weaponCost = tier * 10f; 
    }
}

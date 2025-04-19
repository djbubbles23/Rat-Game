using UnityEngine;
using UnityEngine.UI;

public class INVItem : MonoBehaviour
{
    public diceScriptableObject dice;
    public weaponScriptableObject weapon;
    [SerializeField] Image iconImage;
    //public Sprite[] diceImages = new Sprite[3];
    //public Sprite[] weaponImages = new Sprite[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dice != null)
        {
            iconImage.sprite = dice.icon;
        }
        else if(weapon != null)
        {
            iconImage.sprite = weapon.icon;
        }
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //ITEM DATA//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;

    //ITEM SLOT//
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public GameObject selectedShalder;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }


    public void AddItem(string itemName, int quanity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = quanity;    
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShalder.SetActive(true);
        thisItemSelected = true;
    }

    public void OnRightClick()
    {

    }

}

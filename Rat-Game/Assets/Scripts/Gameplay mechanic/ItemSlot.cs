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
    public string itemDescription;
    public Sprite emptySprite;

    //ITEM SLOT//
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    //ITEM DESCRIPTION SLOT//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;




    public GameObject selectedShalder;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;
    public EquippedSlot[] equippedSlots;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        //equippedSlots = GameObject.Find("EquippedSlots").GetComponent<EquippedSlot[]>();
    }


    public void AddItem(string itemName, int quanity, Sprite itemSprite, string itemDesciption)
    {
        this.itemName = itemName;
        this.quantity = quanity;    
        this.itemSprite = itemSprite;
        this.itemDescription = itemDesciption;
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
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if(itemDescriptionImage.sprite == null)
            itemDescriptionImage.sprite = emptySprite;
    }

    public void OnRightClick()
    {
        foreach (EquippedSlot slot in equippedSlots)
        {
            if (!slot.isFull)
            {
                slot.AddItem(itemName, itemSprite, itemDescription); // transfer data

                // Clear this slot
                itemName = "";
                itemDescription = "";
                itemSprite = emptySprite;
                isFull = false;

                itemImage.sprite = emptySprite;
                quantity = 0;
                quantityText.text = "";
                quantityText.enabled = false;

                Debug.Log("Item moved to equipped slot, original slot cleared.");
                break;
            }
        }
    }

}

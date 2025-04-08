using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    // UI ELEMENTS
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    // ITEM DESCRIPTION UI ELEMENTS
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    // Empty sprite for when the slot is empty
    public Sprite emptySprite;

    public GameObject selectedShalder;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Method to add an item to the equipped slot
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;

        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
    }

    // Method to clear an equipped item (unequip)
    public void ClearSlot()
    {
        itemName = string.Empty;
        quantity = 0;
        itemSprite = null;
        itemDescription = string.Empty;
        isFull = false;

        quantityText.enabled = false;
        itemImage.sprite = emptySprite;  // Set the empty sprite
        itemDescriptionImage.sprite = emptySprite; // Optional: Set the description image to the empty sprite if needed
    }

    // Handle click event (deselect or unequip item)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    // Left click: Deselect or display item details
    private void OnLeftClick()
    {
        inventoryManager.DeselectAllSlotsAndEquipped();
        selectedShalder.SetActive(true);
        thisItemSelected = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
    }

    // Right click: Unequip the item (clear the slot and return it to the inventory)
    private void OnRightClick()
    {
        if (isFull)
        {
            ItemSlot emptySlot = inventoryManager.GetFirstEmptyItemSlot();
            if (emptySlot != null)
            {
                emptySlot.AddItemToInventory(itemName, quantity, itemSprite, itemDescription);
                ClearSlot();  // Clear the equipped slot and show the empty sprite
                Debug.Log($"Item '{itemName}' unequipped and returned to inventory.");
            }
            else
            {
                Debug.Log("No empty inventory slots available.");
            }
        }
    }
}

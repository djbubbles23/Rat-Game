using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    // ITEM SLOT UI ELEMENTS
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    // ITEM DESCRIPTION UI ELEMENTS
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShalder;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;
    private EquippedSlot[] equippedSlots;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        equippedSlots = GameObject.Find("EquippedSlots").GetComponentsInChildren<EquippedSlot>(); // Adjust this based on how you structure your scene
    }

    // Method to add an item to the slot
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        // Update the UI with the item's data
        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

    public void AddItemToInventory(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

    // Handles the click event on the item slot
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

    // Handle left click (display item details)
    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlotsAndEquipped();
        selectedShalder.SetActive(true);
        thisItemSelected = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if (itemDescriptionImage.sprite == null)
            itemDescriptionImage.sprite = emptySprite;
    }

    // Handle right click (transfer to equipped slot)
    public void OnRightClick()
    {
        // Check for an empty equipped slot to place the item
        for (int i = 0; i < equippedSlots.Length; i++)
        {
            if (!equippedSlots[i].isFull)
            {
                equippedSlots[i].AddItem(itemName, quantity, itemSprite, itemDescription);

                // Clear the item slot (empty the slot)
                itemName = string.Empty;
                quantity = 0;
                itemSprite = null;
                itemDescription = string.Empty;

                // Update the UI to reflect the empty slot
                quantityText.enabled = false;
                itemImage.sprite = emptySprite;

                // Optionally, you can update other UI or trigger a message to show the item was equipped
                Debug.Log($"Item '{itemName}' equipped to slot {i + 1}");
                break;
            }
        }
    }
}

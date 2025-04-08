using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA
    public string itemName;
    public Sprite itemSprite;
    public string itemDescription;
    public Sprite emptySprite;
    public bool isFull;

    // UI COMPONENTS
    public Image itemImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;
    public Image itemDescriptionImage;
    public GameObject selectedShalder;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public void AddItem(string name, Sprite sprite, string description)
    {
        itemName = name;
        itemSprite = sprite;
        itemDescription = description;
        isFull = true;

        // Update UI
        if (itemImage != null) itemImage.sprite = sprite;
        if (itemDescriptionNameText != null) itemDescriptionNameText.text = name;
        if (itemDescriptionText != null) itemDescriptionText.text = description;
        if (itemDescriptionImage != null)
            itemDescriptionImage.sprite = sprite != null ? sprite : emptySprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
    }

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
}

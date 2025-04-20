using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class INVManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject InventoryMenu;
    public bool menuActivated;

    private GameObject draggedItem;
    private GameObject lastItemSlot;

    public GameObject weaponSlot;
    public GameObject[] Eslots = new GameObject[3];
    public diceScriptableObject[] EDice = new diceScriptableObject[3];
    public GameObject[] slots = new GameObject[8];

    public GameObject itemPrefab;
    public GameObject itemImage;
    public GameObject itemName;
    public GameObject itemDescription;

    public weaponController weaponController;

    void Update()
    {
        // Dragging logic
        if (draggedItem != null)
        {
            draggedItem.transform.position = Input.mousePosition;
            UpdateDraggedItemUI(draggedItem.GetComponent<INVItem>());
        }

        // Inventory toggle
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);
            Time.timeScale = menuActivated ? 0 : 1;
        }

        updateBoxSprite();

        // Sync dice slots with weaponController
        for (int i = 0; i < Eslots.Length; i++)
        {
            INVSlot Eslot = Eslots[i].GetComponent<INVSlot>();
            if (Eslot.heldItem != null)
            {
                weaponController.diceSlots[i] = Eslot.heldItem.GetComponent<INVItem>().dice;
            }
            else
            {
                weaponController.diceSlots[i] = null;
            }
        }
    }

    private void UpdateDraggedItemUI(INVItem inv)
    {
        if (inv == null) return;

        if (inv.weapon != null)
        {
            itemImage.GetComponent<Image>().sprite = inv.weapon.icon;
            itemName.GetComponent<TextMeshProUGUI>().text = inv.weapon.weaponName;
            itemDescription.GetComponent<TextMeshProUGUI>().text = inv.weapon.weaponDescription;
        }
        else if (inv.dice != null)
        {
            itemImage.GetComponent<Image>().sprite = inv.dice.icon;
            itemName.GetComponent<TextMeshProUGUI>().text = inv.dice.diceName;
            itemDescription.GetComponent<TextMeshProUGUI>().text = inv.dice.diceDescription;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
            INVSlot slot = clickedObject.GetComponent<INVSlot>();

            if (slot != null && slot.heldItem != null)
            {
                draggedItem = slot.heldItem;
                slot.heldItem = null;

                draggedItem.transform.SetParent(InventoryMenu.transform, true);
                draggedItem.transform.localScale = Vector3.one;
                draggedItem.transform.SetAsLastSibling();

                lastItemSlot = clickedObject;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Check if the left mouse button was released
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Get the GameObject under the pointer when the button was released
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

            // Try to get the INVSlot component from the clicked object
            INVSlot slot = clickedObject.GetComponent<INVSlot>();

            // If the clicked object is a valid inventory slot
            if (slot != null)
            {
                // Determine the type of the dragged item (e.g., weapon or dice)
                String itemType = getDraggedItem(draggedItem);

                // Check if the slot is empty
                if (slot.heldItem == null)
                {
                    if (itemType == "weapon" && clickedObject.CompareTag("weaponSlot"))
                    {
                        // If the slot is a weapon slot and the dragged item is a weapon, place it in the slot
                        slot.SetHeldItem(draggedItem);
                        draggedItem.transform.SetParent(slot.transform, false);
                        draggedItem.transform.localPosition = Vector3.zero;
                        draggedItem = null; // Clear the dragged item reference
                    }
                    else if (itemType == "dice" && clickedObject.CompareTag("diceSlot"))
                    {
                        // If the slot is a dice slot and the dragged item is a dice, place it in the slot
                        slot.SetHeldItem(draggedItem);
                        draggedItem.transform.SetParent(slot.transform, false);
                        draggedItem.transform.localPosition = Vector3.zero;
                        draggedItem = null; // Clear the dragged item reference
                    }
                    else if (itemType == null && clickedObject.CompareTag("invSlot"))
                    {
                        // If the slot is an inventory slot and the dragged item is an inventory item, place it in the slot
                        slot.SetHeldItem(draggedItem);
                        draggedItem.transform.SetParent(slot.transform, false);
                        draggedItem.transform.localPosition = Vector3.zero;
                        draggedItem = null; // Clear the dragged item reference
                    }
                    else{
                        // If the dragged item type does not match the slot type, return to original slot
                        setLastItemSlot();
                    }
                }
                else
                {
                    setLastItemSlot();
                }
            }
            else
            {
                // If the clicked object is not a valid inventory slot, return the dragged item to its original slot
                setLastItemSlot();
            }
        }
    }

    public void ItemPicked(GameObject item)
    {
        GameObject emptySlot = null;
        for (int i = 0; i < slots.Length; i++)
        {
            INVSlot slot = slots[i].GetComponent<INVSlot>();
            if (slot.heldItem == null)
            {
                emptySlot = slots[i];
                break;
            }
        }

        if (emptySlot != null)
        {
            GameObject newItem = Instantiate(itemPrefab);
            INVItem newItemComp = newItem.GetComponent<INVItem>();

            // Assign weapon or dice
            if (item.GetComponent<INVWeaponPickup>()?.weapon != null)
                newItemComp.weapon = item.GetComponent<INVWeaponPickup>().weapon;

            if (item.GetComponent<INVItemPickup>()?.dice != null)
                newItemComp.dice = item.GetComponent<INVItemPickup>().dice;

            newItem.transform.SetParent(emptySlot.transform, false);
            newItem.transform.localPosition = Vector3.zero;
            emptySlot.GetComponent<INVSlot>().SetHeldItem(newItem);

            Destroy(item);
        }
        else
        {
            Debug.Log("No empty slots available!");
        }
    }

    public void updateBoxSprite()
    {
        foreach (GameObject slotObj in slots)
        {
            INVSlot slot = slotObj.GetComponent<INVSlot>();
            Image bg = slot.transform.Find("Background").GetComponent<Image>();
            bg.sprite = Resources.Load<Sprite>(slot.heldItem == null ? "Images/Inv_empty_box" : "Images/Inv_hold_box");
        }

        foreach (GameObject slotObj in Eslots)
        {
            INVSlot slot = slotObj.GetComponent<INVSlot>();
            Image bg = slot.transform.Find("Background").GetComponent<Image>();
            bg.sprite = Resources.Load<Sprite>(slot.heldItem == null ? "Images/Inv_empty_box" : "Images/Inv_hold_box_hover");
        }

        INVSlot weaponSlotComp = weaponSlot.GetComponent<INVSlot>();
        Image weaponBg = weaponSlot.transform.Find("Background").GetComponent<Image>();
        weaponBg.sprite = Resources.Load<Sprite>(weaponSlotComp.heldItem == null ? "Images/Inv_empty_box" : "Images/Inv_hold_box_hover");
    }

    public void setLastItemSlot(){
        // Return to original slot if dropped outside
        if (lastItemSlot != null)
        {
            lastItemSlot.GetComponent<INVSlot>().SetHeldItem(draggedItem);
            draggedItem.transform.SetParent(lastItemSlot.transform, false);
            draggedItem.transform.localPosition = Vector3.zero;
        }
        draggedItem = null;
    }

    public String getDraggedItem(GameObject item)
    {
        if (draggedItem.GetComponent<INVItem>().weapon != null)
        {
            return "weapon";
        }
        else if (draggedItem.GetComponent<INVItem>().dice != null)
        {
            return "dice";
        }
        else{
            return null;
        }
    }
}

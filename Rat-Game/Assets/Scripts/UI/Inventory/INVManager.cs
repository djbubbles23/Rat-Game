using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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
        if (draggedItem == null || eventData.button != PointerEventData.InputButton.Left)
            return;

        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        INVSlot slot = clickedObject.GetComponent<INVSlot>();

        if (slot != null)
        {
            if (slot.heldItem == null)
            {
                slot.SetHeldItem(draggedItem);
                draggedItem.transform.SetParent(slot.transform, false);
                draggedItem.transform.localPosition = Vector3.zero;
                draggedItem = null;
            }
            else
            {
                GameObject tempItem = slot.heldItem;
                slot.SetHeldItem(draggedItem);
                lastItemSlot.GetComponent<INVSlot>().SetHeldItem(tempItem);

                draggedItem.transform.SetParent(slot.transform, false);
                draggedItem.transform.localPosition = Vector3.zero;
                draggedItem = null;
            }
        }
        
        else
        {
            setLastItemSlot();
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
}

using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;
    public EquippedSlot[] equippedSlots;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }

       else if (Input.GetKeyDown(KeyCode.Tab) && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;  
        }
    }


    public void AddItem(string itemName, int quanity, Sprite itemSprite, string itemDesciption)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quanity, itemSprite, itemDesciption);
                return;
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShalder.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
    public void DeselectAllEquippedSlots()
    {
        for (int i = 0; i < equippedSlots.Length; i++)
        {
            equippedSlots[i].selectedShalder.SetActive(false);
            equippedSlots[i].thisItemSelected = false;
        }
    }
    public void DeselectAllSlotsAndEquipped()
    {
        DeselectAllSlots();
        DeselectAllEquippedSlots();
    }

    public void MoveItemBackToItemSlot(EquippedSlot equippedSlot)
    {
        // Find an empty Item Slot in the Item Slot array
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull) // Find the first empty slot
            {
                itemSlot[i].AddItem(equippedSlot.itemName, equippedSlot.quantity, equippedSlot.itemSprite, equippedSlot.itemDescription);
                equippedSlot.isFull = false;  // Mark equipped slot as empty
                return;
            }
        }

        Debug.Log("No empty item slot available.");
    }

    public ItemSlot GetFirstEmptyItemSlot()
    {
        foreach (ItemSlot slot in itemSlot) // assuming itemSlot is an array or list of ItemSlot
        {
            if (!slot.isFull)  // Checks if the slot is empty
            {
                return slot;
            }
        }

        return null;  // No empty slot found
    }


}

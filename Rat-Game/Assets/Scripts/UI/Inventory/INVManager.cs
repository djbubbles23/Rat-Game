using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.Animations;

public class INVManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject InventoryMenu;
    public bool menuActivated;
    private GameObject draggedItem;
    private GameObject lastItemSlot;
    [SerializeField] GameObject weaponSlot;
    public GameObject[] Eslots = new GameObject[3];
    public diceScriptableObject[] EDice = new diceScriptableObject[3];
    [SerializeField] GameObject[] slots = new GameObject[8];
    public GameObject itemPrefab;
    public GameObject itemImage;
    public GameObject itemName;
    public GameObject itemDescription;
    public weaponController weaponController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if(draggedItem != null)
        {
            draggedItem.transform.position = Input.mousePosition;
            itemImage.GetComponent<Image>().sprite = draggedItem.GetComponent<INVItem>().dice.icon;
            itemName.GetComponent<TextMeshProUGUI>().text = draggedItem.GetComponent<INVItem>().dice.diceName;
            itemDescription.GetComponent<TextMeshProUGUI>().text = draggedItem.GetComponent<INVItem>().dice.diceDescription;

        }

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

        updateBoxSprite();

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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
            INVSlot slot = clickedObject.GetComponent<INVSlot>();
            Debug.Log(clickedObject.name);
            Debug.Log(slot);

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
        if (draggedItem != null && eventData.pointerCurrentRaycast.gameObject != null && eventData.button == PointerEventData.InputButton.Left)
        {
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
            Debug.Log("Empty Slot: " + emptySlot.name);
            GameObject newItem = Instantiate(itemPrefab);
            
            newItem.GetComponent<INVItem>().dice = item.GetComponent<INVItemPickup>().dice;

            // Get Description
            //itemImage.GetComponent<Image>().sprite = newItem.GetComponent<INVItem>().dice.icon;
            //itemName.GetComponent<Text>().text = newItem.GetComponent<INVItem>().dice.diceName;
            //itemDescription.GetComponent<Text>().text = newItem.GetComponent<INVItem>().dice.diceDescription;

            newItem.transform.SetParent(emptySlot.transform, false);  
            newItem.transform.localPosition = Vector3.zero;

            // Assign the new item to the slot
            emptySlot.GetComponent<INVSlot>().SetHeldItem(newItem);

            Destroy(item); // Destroy the original item
        }
        else
        {
            Debug.Log("No empty slots available!");
        }
    }

    public void updateBoxSprite()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            INVSlot slot = slots[i].GetComponent<INVSlot>();
            Transform backgroundTransform = slot.transform.Find("Background");
            Image backgroundImage = backgroundTransform.GetComponent<Image>();
            if (slot.heldItem == null)
            {
                Sprite sprite = Resources.Load<Sprite>("Images/Inv_empty_box");
                backgroundImage.sprite = sprite;
            }
            else
            {
                Sprite sprite = Resources.Load<Sprite>("Images/Inv_hold_box");
                backgroundImage.sprite = sprite;
            }
        }

        for (int i = 0; i < Eslots.Length; i++)
        {
            INVSlot Eslot = Eslots[i].GetComponent<INVSlot>();
            Transform backgroundTransform = Eslot.transform.Find("Background");
            Image backgroundImage = backgroundTransform.GetComponent<Image>();
            if (Eslot.heldItem == null)
            {
                Sprite sprite = Resources.Load<Sprite>("Images/Inv_empty_box");
                backgroundImage.sprite = sprite;
            }
            else
            {
                Sprite sprite = Resources.Load<Sprite>("Images/Inv_hold_box_hover");
                backgroundImage.sprite = sprite;
            }
        }

        INVSlot weaponSlot = this.weaponSlot.GetComponent<INVSlot>();
        if(weaponSlot.heldItem == null)
        {
            Transform backgroundTransform = this.weaponSlot.transform.Find("Background");
            Image backgroundImage = backgroundTransform.GetComponent<Image>();
            Sprite sprite = Resources.Load<Sprite>("Images/Inv_empty_box");
            backgroundImage.sprite = sprite;
        }
        else
        {
            Transform backgroundTransform = this.weaponSlot.transform.Find("Background");
            Image backgroundImage = backgroundTransform.GetComponent<Image>();
            Sprite sprite = Resources.Load<Sprite>("Images/Inv_hold_box_hover");
            backgroundImage.sprite = sprite;
        }
    }

}
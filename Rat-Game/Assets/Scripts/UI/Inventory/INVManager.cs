using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class INVManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject InventoryMenu;
    private bool menuActivated;
    private GameObject draggedItem;
    private GameObject lastItemSlot;
    [SerializeField] GameObject weaponSlot;
    [SerializeField] GameObject[] Eslots = new GameObject[3];
    [SerializeField] GameObject[] slots = new GameObject[8];
    public GameObject itemPrefab;
    public GameObject itemImage;
    public GameObject itemName;
    public GameObject itemEffect;
    public GameObject itemDescription;
    public GameObject itemCost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if(draggedItem != null)
        {
            draggedItem.transform.position = Input.mousePosition;

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

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
            INVSlot slot = clickedObject.GetComponent<INVSlot>();
            Debug.Log(clickedObject.name);
            Debug.Log(slot);
            if(slot != null && slot.heldItem != null)
            {
                draggedItem = slot.heldItem;
                slot.heldItem = null; 
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


}

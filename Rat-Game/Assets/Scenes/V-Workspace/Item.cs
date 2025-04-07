using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;
    [SerializeField]
    private int quanity;
    [SerializeField]
    private Sprite sprite;

    private InventoryManager inventoryManager;
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Item has been picked up");
            inventoryManager.AddItem(itemName, quanity, sprite);
            Destroy(gameObject);

        }
    }
}

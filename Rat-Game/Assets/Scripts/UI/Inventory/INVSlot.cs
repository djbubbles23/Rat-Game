using UnityEngine;

public class INVSlot : MonoBehaviour
{
    public GameObject heldItem;
    public void SetHeldItem(GameObject item)
    {
        if (item == null)
        {
            Debug.LogError("SetHeldItem: The item passed is null!");
            return;
        }
        heldItem = item;
        heldItem.transform.position = transform.position;
    }
}

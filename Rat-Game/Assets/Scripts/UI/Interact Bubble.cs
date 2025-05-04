using UnityEngine;
using UnityEngine.UI;

public class InteractBubble : MonoBehaviour
{
    public GameObject imageObject; // Assign the UI image GameObject in Inspector
    public string playerTag = "Player"; // Make sure the player GameObject is tagged properly

    void Start()
    {
        if (imageObject != null)
            imageObject.SetActive(false); // Image starts off
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            imageObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            imageObject.SetActive(false);
        }
    }
}

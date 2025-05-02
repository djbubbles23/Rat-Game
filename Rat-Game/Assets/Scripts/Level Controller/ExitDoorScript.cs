using UnityEngine;

public class ExitDoorScript : MonoBehaviour
{
    [SerializeField] private ZoneControllerScript zoneController;
    public GameObject crossFadePrefab;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetAxis("Jump") == 1)
        {
            crossFadePrefab.SetActive(false);
            crossFadePrefab.SetActive(true);
            string nextZone = StateControllerScript.GetNextZone();
            if (!string.IsNullOrEmpty(nextZone))
            {
                zoneController.StartZone();
            }
            else
            {
                Debug.Log("All zones completed.");
            }
        }
    }
}

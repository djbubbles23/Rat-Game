using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetAxis("Jump") == 1)  
        {
            SceneManager.LoadScene("Map Menu");  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>(); 
            if (playerStats != null)
            {
                playerStats.UnlockNextLevel(); 
            }
        }
    }
}

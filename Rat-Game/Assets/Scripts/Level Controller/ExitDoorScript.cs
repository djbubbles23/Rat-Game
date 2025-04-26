using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorScript : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if (Input.GetAxis("Jump") == 1 && other.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene("Map Menu");
        }
    }
}
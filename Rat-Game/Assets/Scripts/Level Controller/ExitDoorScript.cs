using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorScript : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if (Input.GetAxis("Jump") == 1) {
            SceneManager.LoadScene("Map Menu");
        }
    }
}

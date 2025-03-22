using UnityEngine;

public class playerInv : MonoBehaviour
{
   public  static bool GameIsPaused = false;

    public GameObject playerInvUI;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (GameIsPaused)
            {
                Resume();
                Debug.Log("Resume");
            }
            else
            {
                Pause();
                Debug.Log("Pause");
            }
        }
    }

    void Resume()
    {
        playerInvUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    void Pause()
    {
        playerInvUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
}

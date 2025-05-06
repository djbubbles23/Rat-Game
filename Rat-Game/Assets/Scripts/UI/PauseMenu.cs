using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public INVManager invM;
    public GameObject optionsMenuUI; // Reference to the options menu UI

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !invM.menuActivated)
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1; // Ensure time resumes
        GameIsPaused = false;
        optionsMenuUI.SetActive(false); // Hide options menu if it was open
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0; // Freeze time
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1; // Ensure time resumes when going to the main menu
        SceneManager.LoadScene(0);
    }
}

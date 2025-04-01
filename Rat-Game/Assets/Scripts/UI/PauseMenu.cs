using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
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
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    // Function to return to scene 0
    public void ReturnToScene0()
    {
        Time.timeScale = 1; // Make sure time is resumed before loading a new scene
        SceneManager.LoadScene(0); // Load scene 0
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons; // Assign all level buttons in the inspector
    public int[] levelUnlockMapping; // Assign level indexes corresponding to buttons

    private string[] levelNames = { "A-Workspace", "C-Workspace", "J-Workspace" };

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Lock all buttons initially
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        // Unlock level 1 (always unlocked)
        buttons[0].interactable = true;

        // Unlock other levels based on the unlocked level
        for (int i = 0; i < levelUnlockMapping.Length; i++)
        {
            if (levelUnlockMapping[i] < unlockedLevel)
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void OpenLevel(int levelId)
    {
        if (levelId >= 0 && levelId < levelNames.Length)
        {
            SceneManager.LoadScene(levelNames[levelId]);
        }
        else
        {
            Debug.LogError("Invalid level ID: " + levelId);
        }
    }

    private void Update()
    {
        // Check for "P" key press to reset the unlocked level
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetUnlockedLevel();  // Reset the unlocked level to 1 when "P" is pressed
        }
    }

    // Reset unlocked level back to 1
    private void ResetUnlockedLevel()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);  // Set unlocked level to 1
        Debug.Log("Unlocked level reset to 1");

        // Re-lock all levels except for level 1
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != 0) // Keep level 1 unlocked
            {
                buttons[i].interactable = false;  // Lock all buttons except level 1
            }
        }
    }
}

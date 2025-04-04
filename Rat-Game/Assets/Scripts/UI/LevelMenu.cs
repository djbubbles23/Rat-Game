using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Cinemachine;  // Make sure to include Cinemachine namespace

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons; // Assign all level buttons in the inspector
    public int[] levelUnlockMapping; // Assign level indexes corresponding to buttons
    public CinemachineCamera vcam1; // Assign vcam1 in the inspector

<<<<<<< HEAD
<<<<<<< HEAD
    private string[] levelNames = { "C-level-Work", "C-Workspace", "J-Workspace" };
=======
    private string[] levelNames = { "A-Workspace", "C-Workspace", "J-Workspace" };
>>>>>>> parent of 290f4b8 (Merge pull request #10 from djbubbles23/jacob-enemy)
=======
    private string[] levelNames = { "A-Workspace", "C-Workspace", "J-Workspace" };
>>>>>>> parent of 290f4b8 (Merge pull request #10 from djbubbles23/jacob-enemy)
    private bool isVcam1Active = false;

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

    private void Update()
    {
        // Check if vcam1 is active by comparing its priority (higher priority means it's active)
        bool isCurrentlyVcam1Active = vcam1.Priority > 0;

        if (isCurrentlyVcam1Active != isVcam1Active)
        {
            isVcam1Active = isCurrentlyVcam1Active;
            SetButtonsInteractable(!isVcam1Active);
        }

        // Check for "P" key press to reset the unlocked level
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetUnlockedLevel();  // Reset the unlocked level to 1 when "P" is pressed
        }
    }

    private void SetButtonsInteractable(bool state)
    {
        foreach (Button button in buttons)
        {
            button.interactable = state;
        }
    }

    public void OpenLevel(int levelId)
    {
        if (!isVcam1Active && levelId >= 0 && levelId < levelNames.Length)
        {
            SceneManager.LoadScene(levelNames[levelId]);
        }
        else if (isVcam1Active)
        {
            Debug.LogWarning("Cannot open level while vcam1 is active.");
        }
        else
        {
            Debug.LogError("Invalid level ID: " + levelId);
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
